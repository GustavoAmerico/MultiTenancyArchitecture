using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using MultiTenancy.Collections;
using MultiTenancy.Generic;
using MultiTenancy.Generic.Defaults;

namespace MultiTenancy.Tests
{
    [TestClass]
    [TestCategory("Load Tenants")]
    public class TenantCollectionTest
    {
        private TenantClaims<Guid, Dictionary<string, object>> _claimsFirstTenant;
        private Guid _firstTenantId;
        private Guid _secondTenantId;
        private TenantSecret<Guid, Dictionary<string, object>> _secretFirstTenant;
        private TenantSecret<Guid, Dictionary<string, object>> _secretSecondTenant;

        /// <summary>Esse metodo testa se a coleção padrão está permitindo itens duplicados</summary>
        [TestMethod]
        public void AddDuplicatesTenants()
        {
            var id = Guid.NewGuid();
            var tenant1 = new TenantDefault(id, "Tenant 1");
            var tenant2 = new TenantDefault(id, "Tenant 2");

            var tenant = new TenantCollection();
            tenant.Add(tenant1);
            tenant.Add(tenant2);
            tenant.Add((TenantDefault)null);
            Assert.AreEqual(tenant.Count, 1, "A clase permitiu adicionar duas instancias iguais");
        }

        /// <summary>
        /// Esse metodo testa a adições de novos tenants na lista padrão de tenant com ID do tipo GUID
        /// </summary>
        [TestMethod]
        public void AddNewTenants()
        {
            var tenant1 = new TenantDefault(Guid.NewGuid(), "Tenant 1");
            var tenant2 = new TenantDefault(Guid.NewGuid(), "Tenant 2");

            var tenant = new TenantCollection();
            int current = 0;
            Assert.AreEqual(tenant.Count, current, "Ao instanciar uma coleção ela iniciou com itens carregados");
            current++;

            tenant.Add(tenant1);
            Assert.AreEqual(tenant.Count, current, "Após adicionar um item na coleção a lista de itens não foi alterada");

            tenant.Add(tenant2);
            current++;
            Assert.AreEqual(tenant.Count, current, "Após adicionar um segundo item na coleção a lista de itens não foi alterada");
        }

        /// <summary>Esse metodo testa a adição de tenant e tenantsecret na mesma coleção</summary>
        [TestMethod]
        public void AddTenantAndDistinctTenantSecret()
        {
            var tenant1 = new TenantDefault(_firstTenantId, "Tenant 1");
            var tenant = new TenantCollection();
            tenant.Add(tenant1);

            tenant.Add(_secretSecondTenant);

            Assert.AreEqual(tenant.Count, 2);
            //tenantSecret.
        }

        /// <summary>
        /// Esse metodo vai testar a ação de adicionar um tenant e depois mesclar com um claims
        /// carregado de outro provider
        /// </summary>
        [TestMethod]
        public void AddTenantAndMergeTenantClaims()
        {
            var tenant1 = new TenantDefault(_firstTenantId, "Tenant 1");
            var tenant = new TenantCollection();
            tenant.Add(tenant1);

            tenant.Add(new[] { _claimsFirstTenant });

            var tenantk = tenant.FirstOrDefault<Guid, Dictionary<string, object>, int>(_firstTenantId);
            Assert.IsNotNull(tenantk);
            Assert.AreEqual(tenant.Count, 1);
            Assert.IsTrue(tenantk.Claims.ContainsKey("Claims1"));
        }

        /// <summary>
        /// Esse metodo vai testar a ação de adicionar um tenant e depois mesclar com um claims
        /// carregado de outro provider
        /// </summary>
        [TestMethod]
        public void AddTenantAndMergeTenantClaimsAndSecret()
        {
            var tenant1 = new TenantDefault(_firstTenantId, "Tenant 1");
            var tenant = new TenantCollection();
            tenant.Add(tenant1);

            tenant.Add(_claimsFirstTenant);

            tenant.Add(new[] { _secretFirstTenant });
            Console.WriteLine(tenant.ToString());
            Assert.AreEqual(tenant.Count, 1);
        }

        /// <summary>
        /// Esse metodo vai testar a ação de adicionar um tenant e depois mesclar com um secret
        /// carregado de outro provider
        /// </summary>
        [TestMethod]
        public void AddTenantAndMergeTenantSecret()
        {
            var tenant1 = new TenantDefault(_firstTenantId, "Tenant 1");
            var tenant = new TenantCollection();
            tenant.Add(tenant1);

            tenant.Add(_secretFirstTenant);
            Assert.AreEqual(tenant.Count, 1);
        }

        [TestInitialize]
        public void Initialize()
        {
            _firstTenantId = Guid.NewGuid();
            _secondTenantId = Guid.NewGuid();
            var dicSecretFirst = new Dictionary<string, object>() { { "Secret1", "Value 1" } };
            var dicSecretSecond = new Dictionary<string, object>() { { "Secret2", "Value 2" } };
            var dicClaimsFirst = new Dictionary<string, object>() { { "Claims1", "Value 1" } };

            _secretFirstTenant = new TenantSecret<Guid, Dictionary<string, object>>(_firstTenantId, secrets: dicSecretFirst);
            _claimsFirstTenant = new TenantClaims<Guid, Dictionary<string, object>>(_firstTenantId, claims: dicClaimsFirst);

            _secretSecondTenant = new TenantSecret<Guid, Dictionary<string, object>>(_secondTenantId, secrets: dicSecretSecond);
        }

        /// <summary>
        /// Esse metodo verifica se a instancia de tenant é atualizada após ser mesclada com TenantSecret
        /// </summary>
        [TestMethod]
        public void MergeTenantWithSecretAndCheckAllInstancies_ExpectedAllInstanceRelated()
        {
            var id = _secretFirstTenant.Id;
            var tenant1 = new TenantDefault(id, "Tenant 1");
            var tenant2 = new TenantDefault(id, "Tenant 1 - One");
            var tenant3 = new TenantDefault(_secretSecondTenant.Id, "Tenant 3");

            var collection2 = new TenantCollection();
            var itens =
                new ITenantItem<Guid>[] {
                tenant1,
                    tenant2,
                    tenant3,
                    _secretSecondTenant,
                    _secretFirstTenant };

            collection2.Add(itens);

            Assert.IsTrue(tenant3.Secrets?.Equals(_secretSecondTenant.Secrets) == true);

            Assert.AreEqual(2, collection2.Count);
        }

        [TestMethod]
        public void StartTenantCollection()
        {
            var id = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var tenant1 = new TenantDefault(id, "Tenant 1");
            var tenant2 = new TenantDefault(id, "Tenant 2");
            var tenant3 = new TenantDefault(id2, "Tenant 3");

            var secrets2 = new Dictionary<string, string>() { { "Secret2", "Value 1" } };
            var tenantSecret2 = new TenantSecret<Guid, dynamic>(id2, secrets: secrets2);

            var claims = new Dictionary<string, string>() { { "Claims1", "Value 1" } };
            var tenantClaims = new TenantClaims<Guid, dynamic>(id, claims: claims);

            //var collection = new TenantCollection(new[] { tenant1, tenant2, tenant3 });
            //Assert.AreEqual(2, collection.Count);
            //var itens = new ITenantItem<Guid>[] { tenant1, tenant2, tenant3, tenantClaims, _secretFirstTenant, tenantSecret2, null };
            //var collection2 = new TenantCollection();
            //collection2.Add(itens);
            //Assert.AreEqual(3, collection2.Count);

            //Assert.ThrowsException<ArgumentNullException>(() => collection2.Add((IEnumerable<ITenantItem<Guid>>)null));
        }
    }
}