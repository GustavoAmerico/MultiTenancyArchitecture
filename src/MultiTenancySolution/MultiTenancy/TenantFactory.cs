using System;
using System.Collections.Generic;
using System.Linq;
/*
namespace MultiTenancy
{
    public class TenantFactory
    {
        private readonly IEnumerable<ITenantClaimsProvider> _claims;
        private readonly IEnumerable<ITenantSecretProvider> _secrets;
        private readonly IEnumerable<ITenantProvider> _tenants;
        private readonly IServiceProvider provider;

        public TenantFactory(IEnumerable<ITenantClaimsProvider> claims
            , IEnumerable<ITenantProvider> tenants
            , IEnumerable<ITenantSecretProvider> secrets
            , IServiceProvider provider)
        {
            _claims = claims;
            _tenants = tenants;
            _secrets = secrets;
            this.provider = provider;
        }

        public void Build<TKey, TProperty>()
        {
            var tenants = Resolve<TKey>();
            TenantDictionary<TKey, TProperty> properties = _claims.Select(t => t.GetTenantClaims<TKey, TProperty>(provider)).ToArray();
        }

        public void Build<TKey, TProperty, TSecret>()
        {
            var tenants = _tenants.Select(t => t.GetTenants<TKey>(provider));
            TenantDictionary<TKey, TProperty> properties = _claims.Select(t => t.GetTenantClaims<TKey, TProperty>(provider)).ToArray();
            TenantDictionary<TKey, TSecret> secrets = _secrets.Select(t => t.GetTenantSecrets<TKey, TSecret>(provider)).ToArray();
        }

        public TenantCollection<TKey> Resolve<TKey>()
        {
            var tenants = new TenantCollection<TKey>(_tenants.SelectMany(t => t.GetTenants<TKey>(provider)));
            return tenants;
        }
    }
}

*/
