using System.Collections.Generic;

namespace MultiTenancy
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class TenantEqualityComparer<TKey> : EqualityComparer<ITenant<TKey>>
    {
        /// <summary>determines whether two objects are equal.</summary>
        /// <returns>returns true if two are equals</returns>
        public override bool Equals(ITenant<TKey> x, ITenant<TKey> y) => !ReferenceEquals(y, null) && x?.Id?.Equals(y.Id) == true;

        /// <summary> hash function for the specified object for hashing algorithms and data structures, such as a hash table</summary>
        public override int GetHashCode(ITenant<TKey> obj) => obj?.Id?.GetHashCode() ?? -1;
    }
}