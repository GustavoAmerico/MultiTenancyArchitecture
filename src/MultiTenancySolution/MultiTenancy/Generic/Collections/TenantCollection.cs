using MultiTenancy.Defaults;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MultiTenancy
{
    /// <summary>Represents the list of clients associated with the application</summary>
    /// <typeparam name="TKey">The identifier key for tenant</typeparam>
    public class TenantCollection<TKey, TProperty, TSecret>
        : HashSet<ITenant<TKey, TProperty, TSecret>>
        , ITenantCollection<TKey, TProperty, TSecret>
        , ICollection<ITenant<TKey, TProperty, TSecret>>

        where TSecret : new()
        where TProperty : new()

    {
        public TenantCollection() : base(new TenantEqualityComparer<TKey>())
        {
        }

        /// <summary></summary>
        /// <param name="tenants"></param>
        /// <exception cref="System.ArgumentNullException">Throw when <see cref="tenants"/> is null</exception>
        public TenantCollection(IEnumerable<ITenant<TKey, TProperty, TSecret>> tenants)
            : base(new TenantEqualityComparer<TKey>())
        {
            if (ReferenceEquals(null, tenants))
                throw new System.ArgumentNullException(nameof(tenants));

            foreach (var item in tenants)
                AddIfNotExists(item);
        }

        public TenantCollection(IEnumerable<ITenant<TKey>> tenants)
            : base(new TenantEqualityComparer<TKey>())
        {
            if (ReferenceEquals(null, tenants))
                throw new System.ArgumentNullException(nameof(tenants));

            foreach (var item in tenants)
                AddIfNotExists(item);
        }

        public new bool Add(ITenant<TKey, TProperty, TSecret> item)
        {
            if (ReferenceEquals(null, item)) return false;
            return AddIfNotExists(item);
        }

        public void Add(ITenantClaims<TKey, TProperty> tenantClaims)
        {
            if (ReferenceEquals(null, tenantClaims)) return;
            AddIfNotExists(tenantClaims);
        }

        public void Add(params ITenantClaims<TKey, TProperty>[] tenantClaims)
        {
            if (ReferenceEquals(null, tenantClaims)) return;
            foreach (var claim in tenantClaims)
                Add(claim);
        }

        public void Add(ITenantSecrets<TKey, TSecret> tenantSecret)
        {
            if (ReferenceEquals(null, tenantSecret)) return;
            AddIfNotExists(tenantSecret);
        }

        public void Add(params ITenantSecrets<TKey, TSecret>[] claims)
        {
            if (ReferenceEquals(null, claims)) return;
            foreach (var secret in claims)
                Add(secret);
        }

        public ITenant<TKey, TProperty, TSecret> FirstOrDefault(TKey id)
        {
            var tenant = this.FirstOrDefault(f => f.Id.Equals(id)) as ITenant<TKey, TProperty, TSecret>;
            return tenant;
        }

        public override string ToString()
        {
            return $"{Count} tenants configured";
        }

        private bool AddIfNotExists(ITenant<TKey> newTenantSecret)
        {
            if (ReferenceEquals(null, newTenantSecret)) return false;
            var tenant = this.FirstOrDefault(newTenantSecret.Id);
            if (ReferenceEquals(null, tenant))
            {
                tenant = (newTenantSecret is ITenant<TKey, TProperty, TSecret> tenantconvert)
                    ? tenantconvert
                    : new Tenant<TKey, TProperty, TSecret>(newTenantSecret);

                base.Add(tenant);
                return true;
            }

            if (newTenantSecret is ITenantClaims<TKey, TProperty> tenantClaims)
                tenant.Claims = tenantClaims.Claims;

            if (newTenantSecret is ITenantSecrets<TKey, TSecret> tenantSecret)
                tenant.Secrets = tenantSecret.Secrets;

            return false;
        }
    }
}