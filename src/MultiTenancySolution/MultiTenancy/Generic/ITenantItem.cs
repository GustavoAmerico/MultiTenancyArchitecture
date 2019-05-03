using System;

namespace MultiTenancy.Generic
{
    /// <summary>
    /// This class represents a tenant abstraction of the system with custum configs and secrets
    /// </summary>
    public interface ITenantItem<TKey> : ITenantItem, IEquatable<ITenantItem<TKey>>
    {
        /// <summary>Gets the public identifier from this tenant</summary>
        new TKey Id { get; }
    }
}