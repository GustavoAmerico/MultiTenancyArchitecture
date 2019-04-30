using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using MultiTenancy.Defaults;

namespace MultiTenancy.Tests
{
    /// <summary>Essa classe tem o objetivo de testar a utilização dos tenants com <see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/></summary>
    [TestClass, TestCategory("Dependency Injection")]
    public class ServiceCollectionTest
    {
        [TestMethod]
        public void AddTenantCollectionGenericOnServiceCollection()
        {
            var services = new ServiceCollection();
            var provider = services
                .AddTenantCollection<int, List<string>, List<DateTime>>()
                .BuildServiceProvider();
            var tenants = provider.GetService<ITenantCollection<int, List<string>, List<DateTime>>>();
            tenants.Should()
                .NotBeNull("The tenant collection not working")
                ;
        }

        /// <summary>
        /// Esse teste eu utilizo para preencher um tenant collection com ITenantProvider no
        /// container de Dependencia
        /// </summary>
        [TestMethod]
        public void AddTenantCollectionGenericOnServiceCollectionAndAddTenantProvider()
        {
            var su = Substitute.For<Providers.ITenantProvider>();
            var tenant1 = new Tenant<int, List<string>, List<DateTime>>(10, "Tenant 1", new List<string> { { "Claim 01" } });
            var tenant2 = new Tenant<int, List<string>, List<DateTime>>(11, "Tenant 11", new List<string> { { "Claim 02" } });
            var tenant3 = new Tenant<int, List<string>, List<DateTime>>(12, "Tenant 12", new List<string> { { "Claim 03" } });

            var itens = new HashSet<ITenant<int, List<string>, List<DateTime>>> { tenant1, tenant2, tenant3 };
            su.GetTenants<int, List<string>, List<DateTime>>().Returns(itens);

            var provider = new ServiceCollection()
              .AddTenantCollection<int, List<string>, List<DateTime>>()
              .AddSingleton<Providers.ITenantProvider>(su)
              .BuildServiceProvider();

            var tenants = provider.GetService<ITenantCollection<int, List<string>, List<DateTime>>>();

            tenants.Should()
                .NotBeNull("The tenant collection not working")
                .And.NotContainNulls()
                .And.Contain(tenant1)
                .And.HaveCount(3)
            ;
        }

        /// <summary>
        /// Esse teste eu utilizo para preencher um tenant collection com ITenantProvider e
        /// ITenantSecretProvider no container de Dependencia
        /// </summary>
        [TestMethod]
        public void AddTenantCollectionGenericOnServiceCollectionAndAddTenantProviderAndSecret()
        {
            var tenantSecret = new TenantSecret<int, List<DateTime>>(10, new List<DateTime>() { DateTime.Now });

            var tenantProvider = Substitute.For<Providers.ITenantProvider>();
            var tenant1 = new Tenant<int, List<string>, List<DateTime>>(10, "Tenant 1", new List<string> { { "Claim 01" } });
            var tenant2 = new Tenant<int, List<string>, List<DateTime>>(11, "Tenant 11", new List<string> { { "Claim 02" } });
            var tenant3 = new Tenant<int, List<string>, List<DateTime>>(12, "Tenant 12", new List<string> { { "Claim 03" } });

            tenantProvider.GetTenants<int, List<string>, List<DateTime>>()
                .Returns(new HashSet<ITenant<int, List<string>, List<DateTime>>> { tenant1, tenant2, tenant3 });

            var tenantSecretProvider = Substitute.For<Providers.ITenantSecretProvider>();
            tenantSecretProvider.GetTenantSecrets<int, List<DateTime>>()
             .Returns(new HashSet<ITenantSecrets<int, List<DateTime>>>() { tenantSecret });

            var provider = new ServiceCollection()
              .AddTenantCollection<int, List<string>, List<DateTime>>()
              .AddTenantProvider((a) => tenantProvider)
              .AddTenantProvider((a) => tenantSecretProvider)
              // .AddSingleton<Providers.ITenantProvider>(tenantProvider)
              //.AddSingleton<Providers.ITenantSecretProvider>(tenantSecretProvider)
              .BuildServiceProvider();

            var tenants = provider.GetService<ITenantCollection<int, List<string>, List<DateTime>>>();

            tenants.Should()
                .NotBeNull("The tenant collection not working")
                .And.NotContainNulls()
                .And.Contain(tenant1)
                .And.HaveCount(3)
;

            var item = tenants.FirstOrDefault(10);
            item.As<ITenant<int, List<string>, List<DateTime>>>()
                .Secrets.Should()
                .NotBeNull()
                .And.HaveCount(1);
        }
    }
}