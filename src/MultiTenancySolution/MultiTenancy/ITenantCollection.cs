using System;
using System.Collections.Generic;

namespace MultiTenancy
{
    /// <summary>Represents the list of clients associated with the application</summary>
    public interface ITenantCollection : IReadOnlyCollection<ITenant>
    {
    }
}