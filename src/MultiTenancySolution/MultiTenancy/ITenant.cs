using System;
using System.Collections.Generic;

namespace MultiTenancy
{
    /// <summary>This class represents a tenant abstraction of the system</summary>
    public interface ITenant : ITenant<Guid, Dictionary<string, object>, Dictionary<string, object>>
    {
    }
}