using MultiTenancy.Defaults;
using System;
using System.Collections.Generic;

namespace MultiTenancy
{
    /// <summary>This class represents a system tenant instance</summary>
    public class TenantDefault<TKey> : Tenant<TKey, Dictionary<string, object>, Dictionary<string, object>>
    {
        public TenantDefault() : base()
        {
        }

        public TenantDefault(TKey id, string name, bool isEnabled = true) : base(id, name, isEnabled)
        {
        }

        public TenantDefault(ITenant<TKey> tenant) : base(tenant)
        {
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            return base.Equals(obj as ITenant<Guid>);
        }

        public override int GetHashCode() => Id.GetHashCode();

        /// <summary>Gets the text description</summary>
        public override string ToString() => $"{Name} ({Id})";
    }

    public class TenantDefault : TenantDefault<Guid>, ITenant
    {
        public TenantDefault() : base()
        {
        }

        public TenantDefault(Guid id, string name, bool isEnabled = true) : base(id, name, isEnabled)
        {
        }

        public TenantDefault(ITenant<Guid> tenant) : base(tenant)
        {
        }
    }
}