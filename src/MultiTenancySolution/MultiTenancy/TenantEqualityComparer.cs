using System.Collections.Generic;

namespace MultiTenancy
{
    /// <summary></summary>
    /// <typeparam name="TKey"></typeparam>
    public class TenantEqualityComparer : EqualityComparer<ITenantItem>
    {
        /// <summary>determines whether two objects are equal.</summary>
        /// <returns>returns true if two are equals</returns>
        public override bool Equals(ITenantItem x, ITenantItem y) => !ReferenceEquals(y, null) && x?.Equals(y) == true;

        /// <summary>
        /// hash function for the specified object for hashing algorithms and data structures, such
        /// as a hash table
        /// </summary>
        public override int GetHashCode(ITenantItem obj) => obj?.GetHashCode() ?? -1;
    }
}