using System;
using System.Collections.Generic;

namespace MultiTenancy
{
    /// <summary>Represents the list of clients associated with the application</summary>
    public class TenantCollection : TenantCollection<Guid, Dictionary<string, object>, Dictionary<string, object>>, ITenantCollection
    {
        public TenantCollection()
        {
        }

        public TenantCollection(IEnumerable<ITenant<Guid, Dictionary<string, object>, Dictionary<string, object>>> tenants)
            : base(tenants)
        {
        }

        public TenantCollection(IEnumerable<ITenant<Guid>> tenants) : base(tenants)
        {
        }
    }
}