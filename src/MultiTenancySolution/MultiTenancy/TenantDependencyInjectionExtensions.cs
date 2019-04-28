using MultiTenancy;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TenantDependencyInjectionExtensions
    {
        public static IServiceCollection AddTenantCollection<TKey, TClaims, TSecret>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var descriptor = new ServiceDescriptor(
                typeof(ITenantCollection<TKey, TClaims, TSecret>)
                , typeof(TenantCollection<TKey, TClaims, TSecret>)
                , serviceLifetime);
            services.Add(descriptor);

            var desc = new ServiceDescriptor(
                  typeof(ITenantCollection)
                , (provider) => provider.GetService<TenantCollection<TKey, TClaims, TSecret>>()
                , serviceLifetime);
            services.Add(desc);
            return services;
        }

        public static IServiceCollection AddTenantCollection(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var descriptor = new ServiceDescriptor(
                typeof(ITenantCollection<Guid, Dictionary<string, object>, Dictionary<string, object>>)
                , typeof(TenantCollection<Guid, Dictionary<string, object>, Dictionary<string, object>>)
                , serviceLifetime);
            services.Add(descriptor);

            var desc = new ServiceDescriptor(
                  typeof(ITenantCollection)
                , (provider) => provider.GetService<TenantCollection<Guid, Dictionary<string, object>, Dictionary<string, object>>>()
                , serviceLifetime);
            services.Add(desc);
            return services;
        }
    }
}