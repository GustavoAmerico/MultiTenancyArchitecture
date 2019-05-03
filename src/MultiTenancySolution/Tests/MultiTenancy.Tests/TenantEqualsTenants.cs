using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiTenancy.Generic;
using MultiTenancy.Generic.Defaults;

namespace MultiTenancy.Tests
{
    [TestClass, TestCategory("Comparer Tenant")]
    public class TenantEqualsTenants
    {
        [TestMethod]
        public void CheckIfTwoBaseTenantsSomeIdAreEquals()
        {
            var tenant1 = new Tenant<int, string, string>(19, "Tenant 01", "Claims", "Secrets");
            var tenant2 = new Tenant<int, string, string>(19, "Tenant 02", "Claims", "Secrets");
            Assert.IsTrue(tenant1.Equals(tenant2), "Dois tenant com o mesmo ID não estão aparecendo iguais");
            Assert.AreEqual(tenant1.GetHashCode(), tenant2.GetHashCode(), "Dois tenants iguais não estão geram o mesmo hascode");
        }

        [TestMethod]
        public void CheckIfTwoBaseTenantsSomeIdAreEquals_TheOtherTenantIsAbstractType()
        {
            var tenant1 = new Tenant<int, string, string>(19, "Tenant 01", "Claims", "Secrets");
            ITenantItem<int> tenant2 = new Tenant<int, string, string>(19, "Tenant 02", "Claims", "Secrets");
            Assert.IsTrue(tenant1.Equals(tenant2), "Dois tenant com o mesmo ID não estão aparecendo iguais");
        }
    }
}