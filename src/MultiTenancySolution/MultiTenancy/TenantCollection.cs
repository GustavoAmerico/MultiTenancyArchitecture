using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiTenancy
{
    /// <summary>Represents the list of clients associated with the application</summary>
    public class TenantCollection : TenantCollection<Guid, Dictionary<string, object>, Dictionary<string, object>>, ITenantCollection
    {
        int IReadOnlyCollection<ITenant>.Count => throw new NotImplementedException();

        public TenantCollection() : base()
        {
        }

        public TenantCollection(IEnumerable<ITenant<Guid, Dictionary<string, object>, Dictionary<string, object>>> tenants)
    : base()
        {
            Add(tenants);
        }

        IEnumerator<ITenant> IEnumerable<ITenant>.GetEnumerator()
        {
            return this.OfType<ITenant>().GetEnumerator();
        }
    }
}