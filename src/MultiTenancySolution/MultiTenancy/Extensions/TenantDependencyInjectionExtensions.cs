using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MultiTenancy.Collections;

namespace MultiTenancy
{
    public static class TenantDependencyInjectionExtensions
    {
        public static IServiceCollection AddTenantCollection<TKey, TClaims, TSecret>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var descriptor = new ServiceDescriptor(serviceType: typeof(TenantCollection), typeof(TenantCollection), (ServiceLifetime)serviceLifetime);
            services.Add(descriptor);

            var desc = new ServiceDescriptor(typeof(ITenantCollection), (provider) => provider.GetService<TenantCollection>()
                , serviceLifetime);
            services.Add(desc);
            return services;
        }

        public static IServiceCollection AddTenantCollection(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var descriptor = new ServiceDescriptor(
                typeof(ITenantCollection)
                , typeof(TenantCollection)
                , serviceLifetime);
            services.Add(descriptor);

            var desc = new ServiceDescriptor(
                  typeof(ITenantCollection)
                , (provider) => provider.GetService<TenantCollection>()
                , serviceLifetime);
            services.Add(desc);
            return services;
        }

        public static IServiceCollection AddTenantProvider(this IServiceCollection services
            , Func<IServiceProvider, MultiTenancy.Providers.ITenantProvider> createInstance
            , ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.Add(new ServiceDescriptor(
                typeof(MultiTenancy.Providers.ITenantProvider)
                , (provider) => createInstance(provider)
                , serviceLifetime));
            return services;
        }

        public static IServiceCollection AddTenants<TProvider>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TProvider : MultiTenancy.Providers.ITenantProvider
        {
            services.Add(new ServiceDescriptor(typeof(MultiTenancy.Providers.ITenantProvider), typeof(TProvider), serviceLifetime));
            return services;
        }
    }
}