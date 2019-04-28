using System;
using System.Collections.Generic;

namespace MultiTenancy
{
    /// <summary>This class represents a tenant abstraction of the system</summary>
    public interface ITenant //: ITenant<Guid, Dictionary<string, object>, Dictionary<string, object>>
    {
        /// <summary>obtains a flag indicating whether the tenant is active or not</summary>
        bool IsEnabled { get; }

        /// <summary>Gets the public display name from this tenant.</summary>
        string Name { get; }
    }
}