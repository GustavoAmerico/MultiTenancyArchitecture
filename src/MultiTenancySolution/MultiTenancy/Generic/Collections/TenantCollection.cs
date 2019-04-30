using MultiTenancy.Defaults;
using MultiTenancy.Providers;
using System.Collections.Generic;
using System.Linq;

namespace MultiTenancy
{
    /// <summary>Represents the list of clients associated with the application</summary>
    /// <typeparam name="TKey">The identifier key for tenant</typeparam>
    public class TenantCollection<TKey, TClaims, TSecret>
        : HashSet<ITenant<TKey, TClaims, TSecret>>
        , ITenantCollection<TKey, TClaims, TSecret>
        , ICollection<ITenant<TKey, TClaims, TSecret>>
    {
        public TenantCollection() : base(new TenantEqualityComparer<TKey>())
        {
        }

        /// <summary></summary>
        /// <param name="tenants"></param>
        /// <exception cref="System.ArgumentNullException">
        /// Throw when <see cref="tenants"/> is null
        /// </exception>
        public TenantCollection(IEnumerable<ITenantClaimsProvider> claims
            , IEnumerable<ITenantProvider> tenants
            , IEnumerable<ITenantSecretProvider> secrets) : base(new TenantEqualityComparer<TKey>())
        {
            Add(tenants.ToItens<TKey, TClaims, TSecret>());
            Add(claims.ToItens<TKey, TClaims>());
            Add(secrets.ToItens<TKey, TSecret>());
        }

        public void Add(IEnumerable<ITenant<TKey>> tenants)

        {
            if (ReferenceEquals(null, tenants))
                throw new System.ArgumentNullException(nameof(tenants));

            foreach (var item in tenants)
                AddIfNotExists(item);
        }

        public void Add(IEnumerable<ITenant<TKey, TClaims, TSecret>> tenantClaims)
        {
            if (ReferenceEquals(null, tenantClaims)) return;
            foreach (var claim in tenantClaims)
                Add(claim);
        }

        public new bool Add(ITenant<TKey, TClaims, TSecret> item)
        {
            if (ReferenceEquals(null, item)) return false;
            return AddIfNotExists(item);
        }

        public void Add(ITenantClaims<TKey, TClaims> tenantClaims)
        {
            if (ReferenceEquals(null, tenantClaims)) return;
            AddIfNotExists(tenantClaims);
        }

        public void Add(IEnumerable<ITenantClaims<TKey, TClaims>> tenantClaims)
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

        public void Add(IEnumerable<ITenantSecrets<TKey, TSecret>> claims)
        {
            if (ReferenceEquals(null, claims)) return;
            foreach (var secret in claims)
                Add(secret);
        }

        /// <summary>Obtém o tenant com o identificador especificado na consulta</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ITenant<TKey, TClaims, TSecret> FirstOrDefault(TKey id)
        {
            var tenant = this.FirstOrDefault(f => f.Id.Equals(id)) as ITenant<TKey, TClaims, TSecret>;
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
                tenant = (newTenantSecret is ITenant<TKey, TClaims, TSecret> tenantconvert)
                    ? tenantconvert
                    : new Tenant<TKey, TClaims, TSecret>(newTenantSecret);

                base.Add(tenant);
                return true;
            }

            if (newTenantSecret is ITenantClaims<TKey, TClaims> tenantClaims)
                tenant.Claims = tenantClaims.Claims;

            if (newTenantSecret is ITenantSecrets<TKey, TSecret> tenantSecret)
                tenant.Secrets = tenantSecret.Secrets;

            return false;
        }
    }
}