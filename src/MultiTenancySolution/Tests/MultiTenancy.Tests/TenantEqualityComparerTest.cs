using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiTenancy.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiTenancy.Tests
{
    [TestClass, TestCategory("Comparer Tenant")]
    public class TenantEqualityComparerTest
    {
        private TenantClaims<Guid, Dictionary<string, object>> _claimsFirstTenant;
        private Guid _firstTenantId;
        private TenantSecret<Guid, Dictionary<string, object>> _secretFirstTenant;

        /// <summary>Determina se dois tenant são iguais com base na classe <see cref="TenantEqualityComparer{TKey}"/></summary>
        [TestMethod]
        public void AreEquals()
        {
            var tenant1 = new TenantDefault<int>(1, "Tenant 1");
            var tenant2 = new Tenant<int, string, DateTime>(1, "Tenant 2") { Secrets = DateTime.Now, Claims = "Claims 01" };
            var tenant3 = new Tenant<int, DateTime, string>(1, "Tenant 3") { Claims = DateTime.Now, Secrets = "Secrets 01" };

            var list = new ITenant<int>[] { tenant1, tenant2, tenant3 }.ToHashSet(new TenantEqualityComparer<int>());
            Assert.AreEqual(1, list.Count);

            var list2 = new ITenant<int>[] { tenant1, tenant2, tenant3 }.ToHashSet(new TenantEqualityComparer<int, string, DateTime>());
            Assert.AreEqual(3, list2.Count);
        }

        [TestMethod]
        public void CheckIfTenantClaimsWithDistinctTypeAreEquals()
        {
            var defaultId = 19;
            var tenantSecret1 = new TenantClaims<int, DateTime>(defaultId, DateTime.Today);
            var tenantSecret3 = new TenantClaims<int, int>(defaultId, 100);
            Assert.IsFalse(tenantSecret1.Equals(tenantSecret3), "O secret de outro tipo não deve ser considerado iguais mesmo que tenha o mesmo id");
        }

        /// <summary>Esse metodo verifica se um tenant secret e um tenant claims com mesmo ID são iguais pela logica do TenantEqualityComparer </summary>
        [TestMethod]
        public void CheckIfTenantSecretAndTenantClaimsAreEquals()
        {
            var comparer = new TenantEqualityComparer<Guid>();
            var tenant = new TenantDefault(_secretFirstTenant);
            var tenant1 = new TenantDefault(_claimsFirstTenant);

            Assert.IsTrue(comparer.Equals(tenant, tenant1));
        }

        /// <summary>Esse metodo verifica se um tenant secret e um tenant claims com mesmo ID são iguais pela logica do TenantEqualityComparer </summary>
        [TestMethod]
        public void CheckIfTenantSecretAndTenantClaimsAreEquals_ExpectedTenantEqualityComparerResultTrue()
        {
            var comparer = new TenantEqualityComparer<Guid, Dictionary<string, object>, Dictionary<string, object>>();
            var tenant = new TenantDefault(_secretFirstTenant);
            var tenant1 = new TenantDefault(_claimsFirstTenant);

            Assert.IsTrue(comparer.Equals(tenant, tenant1));
        }

        [TestMethod]
        public void CheckIfTenantSecretWithDistinctTypeAreEquals()
        {
            var defaultId = 19;
            var tenantSecret2 = new TenantSecret<int, DateTime>(defaultId);
            var tenantSecret3 = new TenantSecret<int, int>(defaultId);
            Assert.IsFalse(tenantSecret2.Equals(tenantSecret3), "O secret de outro tipo não deve ser considerado iguais mesmo que tenha o mesmo id");
        }

        [TestInitialize]
        public void Initialize()
        {
            _firstTenantId = Guid.NewGuid();
            //     _secondTenantId = Guid.NewGuid();
            var dicSecretFirst = new Dictionary<string, object>() { { "Secret1", "Value 1" } };
            var dicClaimsFirst = new Dictionary<string, object>() { { "Claims1", "Value 1" } };

            _secretFirstTenant = new TenantSecret<Guid, Dictionary<string, object>>(_firstTenantId, secrets: dicSecretFirst);
            _claimsFirstTenant = new TenantClaims<Guid, Dictionary<string, object>>(_firstTenantId, claims: dicClaimsFirst);

            // var dicSecretSecond = new Dictionary<string, object>() { { "Secret2", "Value 2" } };
            //         _secretSecondTenant = new TenantSecret<Guid, Dictionary<string, object>>(_secondTenantId, secrets: dicSecretSecond);
        }

        [TestMethod]
        public void TenantClaimsComparerWithKey()
        {
            var defaultId = 19;
            var tenantSecret1 = new TenantClaims<int, DateTime>(defaultId, DateTime.Today);
            var tenantSecret2 = new TenantClaims<int, DateTime>(defaultId, DateTime.Now);

            Assert.IsTrue(tenantSecret1.Equals(tenantSecret2), "The tenant claims not is equals by ID");

            Assert.IsFalse(tenantSecret1.Equals("Not is valid type"), "Somente ITenantSecret e ITenant podem ser iguais");
            Assert.AreEqual(defaultId.GetHashCode(), tenantSecret1.GetHashCode(), "O Tenant não está retornando o hascode do tipo do secret");
        }

        [TestMethod]
        public void TenantSecretComparerWithKey()
        {
            var defaultId = 19;
            var tenantSecret1 = new TenantSecret<int, DateTime>(defaultId, DateTime.Today);
            var tenantSecret2 = new TenantSecret<int, DateTime>(defaultId);

            Assert.IsTrue(tenantSecret1.Equals(tenantSecret2), "The tenant secret not is equals by ID");
            Assert.IsFalse(tenantSecret1.Equals("Not is valid type"), "Somente ITenantSecret e ITenant podem ser iguais");
            Assert.AreEqual(defaultId.GetHashCode(), tenantSecret1.GetHashCode(), "O Tenant não está retornando o hascode do tipo do secret");
        }
    }
}