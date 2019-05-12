using MultiTenancy.Generic;
using MultiTenancy.Providers;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MultiTenancy.Collections
{
    /// <summary>Represents the list of clients associated with the application</summary>
    /// <typeparam name="TKey">The identifier key for tenant</typeparam>
    public class TenantCollection : HashSet<ITenantItem>, ITenantCollection
    {
        public TenantCollection() : base(new TenantEqualityComparer())
        {
        }

        /// <summary></summary>
        /// <param name="tenants"></param>
        /// <exception cref="System.ArgumentNullException">
        /// Throw when <see cref="tenants"/> is null
        /// </exception>
        public TenantCollection(IEnumerable<ITenantProvider> tenants) : base(new TenantEqualityComparer())
        {
            // Add(tenants.ToItens<TKey, TClaims, TSecret>());
            //Add(claims.ToItens<TKey, TClaims>());
            // Add(secrets.ToItens<TKey, TSecret>());
        }

        public void Add<TKey>(IEnumerable<ITenantItem<TKey>> tenants)

        {
            if (ReferenceEquals(null, tenants))
                throw new System.ArgumentNullException(nameof(tenants));

            foreach (var item in tenants)
                AddIfNotExists(item);
        }

        public new bool Add(ITenantItem item)
        {
            if (ReferenceEquals(item, null)) return false;
            return AddIfNotExists(item);
        }

        public void Add<TKey, TClaims, TSecret>(IEnumerable<ITenant<TKey, TClaims, TSecret>> tenantClaims)
        {
            if (ReferenceEquals(null, tenantClaims)) return;
            foreach (var claim in tenantClaims)
                Add(claim);
        }

        public bool Add<TKey, TClaims, TSecret>(ITenant<TKey, TClaims, TSecret> item)
        {
            if (ReferenceEquals(null, item)) return false;
            return AddIfNotExists<TKey, TClaims, TSecret>(item);
        }

        public void Add<TKey, TClaims>(ITenantClaims<TKey, TClaims> tenantClaims)
        {
            if (ReferenceEquals(null, tenantClaims)) return;
            AddIfNotExists(tenantClaims, out var tenant);
            Merge(tenant, tenantClaims);
        }

        public void Add<TKey, TClaims>(IEnumerable<ITenantClaims<TKey, TClaims>> tenantClaims)
        {
            if (ReferenceEquals(null, tenantClaims)) return;
            foreach (var claim in tenantClaims)
                Add<TKey, TClaims>(claim);
        }

        public void Add<TKey, TSecret>(ITenantSecrets<TKey, TSecret> tenantSecret)
        {
            if (ReferenceEquals(null, tenantSecret)) return;
            AddIfNotExists(tenantSecret, out var tenant);
            Merge(tenant, tenantSecret);
        }

        public void Add<TKey, TSecret>(IEnumerable<ITenantSecrets<TKey, TSecret>> claims)
        {
            if (ReferenceEquals(null, claims)) return;
            foreach (var secret in claims)
                Add(secret);
        }

        /// <summary>Obtém o tenant com o identificador especificado na consulta</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ITenant<TKey, TClaims, TSecret> FirstOrDefault<TKey, TClaims, TSecret>(TKey id)
        {
            var tenant = this.OfType<ITenant<TKey, TClaims, TSecret>>()
                .FirstOrDefault(f => f.Id.Equals(id));
            return tenant;
        }

        /// <summary>Obtém o tenant com o identificador especificado na consulta</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ITenantItem<TKey> FirstOrDefault<TKey>(TKey id)
        {
            var tenant = this.OfType<ITenantItem<TKey>>()
                .FirstOrDefault(f => f.Id.Equals(id));
            return tenant;
        }

        IEnumerator<ITenantItem> IEnumerable<ITenantItem>.GetEnumerator()
        {
            return this.OfType<ITenant>().GetEnumerator();
        }

        public virtual IEnumerator<ITenantItem<TKey>> GetEnumerator<TKey>() => this.OfType<ITenantItem<TKey>>().GetEnumerator();

        /// <summary>
        /// Retorna um enumerator capaz de percorrer a lista de configurações associadas ao tenant
        /// </summary>
        /// <typeparam name="TKey">Representa o tipo da chave que identifica o tenant</typeparam>
        /// <typeparam name="TClaims">Representa o tipo da configuração utilizada pelo tenant</typeparam>
        public virtual IEnumerator<ITenantClaims<TKey, TClaims>> GetEnumeratorClaims<TKey, TClaims>()
        {
            return this.OfType<ITenantClaims<TKey, TClaims>>().GetEnumerator();
        }

        /// <summary>
        /// Retorna um enumerator capaz de percorrer a lista de segredos associados ao tenant
        /// </summary>
        /// <typeparam name="TKey">Representa o tipo da chave que identifica o tenant</typeparam>
        /// <typeparam name="TSecrets">Representa o tipo da configuração utilizada pelo tenant</typeparam>
        public virtual IEnumerator<ITenantSecrets<TKey, TSecrets>> GetEnumeratorSecrets<TKey, TSecrets>()
        {
            return this.OfType<ITenantSecrets<TKey, TSecrets>>().GetEnumerator();
        }

        /// <summary>Retorna um texto representando o total de tenants configurados</summary>
        public override string ToString()
        {
            return $"{Count} tenants configured";
        }

        protected virtual void Merge<TKey, TClaims>(ITenantItem<TKey> tenant, ITenantClaims<TKey, TClaims> tenantClaims)
        {
            if (ReferenceEquals(null, tenantClaims) || ReferenceEquals(null, tenantClaims.Claims)) return;

            if (tenant is ITenantClaims<TKey, TClaims> currentTenantClaims)
            {
                currentTenantClaims.Claims = tenantClaims.Claims;
            }
            else
            {
                var newTenant = new Tenant<TKey, TClaims, object>(tenantClaims);
                newTenant.Clone(tenant);
                //Remove o tenant basico
                base.Remove(tenant);

                // Adiciona o tenant especifico, com a claim
                base.Add(newTenant);
            }
        }

        protected virtual void Merge<TKey, TSecrets>(ITenantItem<TKey> tenant, ITenantSecrets<TKey, TSecrets> newTenantSecret)
        {
            if (tenant is ITenantSecrets<TKey, TSecrets> current && !ReferenceEquals(null, newTenantSecret))
                current.Secrets = newTenantSecret.Secrets;
        }

        private bool AddIfNotExists(ITenantItem newTenantSecret)
        {
            if (ReferenceEquals(null, newTenantSecret)) return false;
            var tenant = this.FirstOrDefault(newTenantSecret.Id) as ITenant;
            if (ReferenceEquals(null, tenant))
            {
                tenant = (newTenantSecret is ITenant tenantconvert)
                    ? tenantconvert
                    : new Tenant<object>(newTenantSecret.Id);

                base.Add(tenant);
                return true;
            }

            return false;
        }

        private void AddIfNotExists<TKey>(ITenantItem<TKey> newTenantSecret, out ITenantItem<TKey> tenant)
        {
            tenant = null;
            if (ReferenceEquals(null, newTenantSecret)) return;

            tenant = this.FirstOrDefault(newTenantSecret.Id);
            if (!ReferenceEquals(null, tenant)) return;

            if (newTenantSecret is ITenant details)
            {
                tenant = newTenantSecret;
                base.Add(details);
            }
            else
            {
                base.Add(newTenantSecret);
            }
        }

        private bool AddIfNotExists<TKey, TClaims, TSecret>(ITenantItem<TKey> newTenantSecret)
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

            Merge(tenant, newTenantSecret as ITenantClaims<TKey, TClaims>);
            Merge(tenant, newTenantSecret as ITenantSecrets<TKey, TSecret>);

            return false;
        }
    }
}