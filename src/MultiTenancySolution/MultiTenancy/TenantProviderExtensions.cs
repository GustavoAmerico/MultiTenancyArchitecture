using System.Collections.Generic;
using System.Linq;

namespace MultiTenancy
{
    /// <summary>Essa classe extende as funcionalidades do provider de tenant</summary>
    public static class TenantProviderExtensions
    {
        public static IEnumerable<ITenantClaims<TKey, TClaims>> ToItens<TKey, TClaims>(this IEnumerable<Providers.ITenantClaimsProvider> providers)
        {
            if (providers?.Any() != true)
                return new ITenantClaims<TKey, TClaims>[0];

            return providers
                .Where(p => !ReferenceEquals(null, p)).SelectMany(p => p.GetTenantClaims<TKey, TClaims>().Where(t => !ReferenceEquals(null, t)));
        }

        public static IEnumerable<ITenantSecrets<TKey, TSecret>> ToItens<TKey, TSecret>(this IEnumerable<Providers.ITenantSecretProvider> providers)
        {
            if (providers?.Any() != true)
                return new ITenantSecrets<TKey, TSecret>[0];

            return providers
                .Where(p => !ReferenceEquals(null, p)).SelectMany(p => p.GetTenantSecrets<TKey, TSecret>().Where(t => !ReferenceEquals(null, t)));
        }

        public static IEnumerable<ITenant<TKey, TClaims, TSecret>> ToItens<TKey, TClaims, TSecret>(this IEnumerable<Providers.ITenantProvider> providers)
        {
            if (providers?.Any() != true)
                return new ITenant<TKey, TClaims, TSecret>[0];

            return providers
                .Where(p => !ReferenceEquals(null, p))
                .SelectMany(p => p.GetTenants<TKey, TClaims, TSecret>())
                .Where(t => !ReferenceEquals(null, t));
        }
    }
}