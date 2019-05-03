using System;

namespace MultiTenancy
{
    /// <summary>This class represents a tenant abstraction of the system</summary>
    public interface ITenantItem : IEquatable<ITenantItem>
    {
        object Id { get; }
    }
}