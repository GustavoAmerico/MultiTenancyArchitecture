using System;

namespace MultiTenancy.Generic
{
    public class TenantDefault : Tenant<System.Guid>
    {
        public TenantDefault(Guid id, string name) : base(id, name)
        {
        }

        public TenantDefault()
        {
        }
    }
}