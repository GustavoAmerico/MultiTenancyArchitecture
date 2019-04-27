using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiTenancy.Defaults;
using System;
using System.Collections.Generic;

namespace MultiTenancy.Tests
{
    [TestClass]
    [TestCategory("Load Tenants")]
    public class TenantInitializeTest
    {
        /// <summary></summary>
        [TestMethod]
        public void InitializeTenantDefault()
        {
            var tenant = new TenantDefault();
            Assert.AreEqual(tenant.Id, Guid.Empty, "O identificador padrão do tenant não corresponde ao valor padrão");

            Assert.AreEqual(tenant.Name, string.Empty, "O nome padrão do tenant não corresponde uma string vazia");

            Assert.IsTrue(tenant.IsEnabled, "O tenant padrão não está iniciando ativa");

            Assert.AreEqual(tenant.Secrets, null, "Os segredos deveriam iniciar null");

            Assert.AreEqual(tenant.Claims, null, "As claims deveriam iniciar null");
        }

        [TestMethod]
        public void InitializeTenantDefaultWithNewValues()
        {
            const string tenantName = " Tenant 01 ";
            Guid id = Guid.NewGuid();
            var tenant = new TenantDefault(id, tenantName);
            Assert.AreEqual(tenantName.Trim(), tenant.Name, "The object name can't start or finished with space");
            Assert.IsTrue(tenant.IsEnabled, "por padrão, um tenant deve iniciar ativo");
            Assert.IsNull(tenant.Secrets, "Não foi definido um secrets para esse tenant");
            Assert.IsNull(tenant.Claims, "Não foi definido um claims para esse tenant");
        }

        [TestMethod]
        public void InitializeTenantWithAllProperties()
        {
            var claims = new Dictionary<string, object>() { { "Claims 01", "Value 01" } };
            var secrets = new Dictionary<string, object>() { { "secrets 01", "Value 01" } };
            var tenannt = new TenantDefault(Guid.NewGuid(), "New Tenant 01", claims, secrets);
            Assert.AreEqual(claims, tenannt.Claims);
            Assert.AreEqual(secrets, tenannt.Secrets);
        }

        [TestMethod]
        public void InitializeTenantWithDefaultClaims()
        {
            Guid id = Guid.NewGuid();
            var dic = new TenantClaims<Guid, (int Id, string Name)>(id, (19, "Claims Test"));
            var tenant = new Tenant<Guid, (int Id, string Name), int>(dic);
            Assert.AreEqual(id, tenant.Id, "O Tenant não iniciou com o identificador da configuração");
            Assert.IsNotNull(tenant.Claims, "O claims não foi iniciado com o valor preenchido");
            Assert.AreEqual(tenant.Claims.Id, 19, "O Claims foi iniciado mas o valor não é igual ao enviado");
        }

        [TestMethod]
        public void InitializeTenantWithDefaultClaimsValues()
        {
            Guid id = Guid.NewGuid();
            var tenant = new Tenant<Guid, (int Id, string Name), int>(id, "Tenant with claims", (19, "Claims Test"));
            Assert.AreEqual(id, tenant.Id, "O Tenant não iniciou com o identificador da configuração");
            Assert.IsNotNull(tenant.Claims, "O claims não foi iniciado com o valor preenchido");
            Assert.AreEqual(tenant.Claims.Id, 19, "O Claims foi iniciado mas o valor não é igual ao enviado");
        }

        [TestMethod]
        public void InitializeTenantWithDefaultSecrets()
        {
            Guid id = Guid.NewGuid();
            var dic = new TenantSecret<Guid, (int Id, string Name)>(id, (19, "Secret Test"));
            var tenant = new Tenant<Guid, int, (int Id, string Name)>(dic);
            Assert.AreEqual(id, tenant.Id, "O Tenant não iniciou com o identificador da configuração");
            Assert.IsNotNull(tenant.Secrets, "O Secret não foi iniciado com o valor preenchido");
            Assert.AreEqual(tenant.Secrets.Id, 19, "O Secret foi iniciado mas o valor não é igual ao enviado");
        }

        [TestMethod]
        public void InitializeTenantWithDefaultSecretsValue()
        {
            Guid id = Guid.NewGuid();
            var tenant = new Tenant<Guid, int, (int Id, string Name)>(id, "New Tenant", (19, "Secret Test"));
            Assert.AreEqual(id, tenant.Id, "O Tenant não iniciou com o identificador da configuração");
            Assert.IsNotNull(tenant.Secrets, "O Secret não foi iniciado com o valor preenchido");
            Assert.AreEqual(tenant.Secrets.Id, 19, "O Secret foi iniciado mas o valor não é igual ao enviado");
        }
    }
}