using System.Collections.Generic;
using System.Linq;
using MultiTenancy.Generic;

namespace MultiTenancy
{
    /// <summary>Essa classe extende as funcionalidades do provider de tenant</summary>
    public static class TenantProviderExtensions
    {
        public static IEnumerable<ITenantItem<TKey>> ToItens<TKey>(this IEnumerable<Providers.ITenantProvider> providers)
        {
            if (providers?.Any() != true)
                return new ITenantItem<TKey>[0];

            return providers
                .Where(p => !ReferenceEquals(null, p))
                .SelectMany(p => p.GetTenants<TKey>())
                .Where(t => !ReferenceEquals(null, t));
        }
    }
}