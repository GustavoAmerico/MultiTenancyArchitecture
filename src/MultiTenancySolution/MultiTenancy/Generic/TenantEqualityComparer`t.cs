using System.Collections.Generic;

namespace MultiTenancy.Generic
{
    public class TenantEqualityComparer<TKey, TProperty, TSecret> : TenantEqualityComparer<TKey>
        , IEqualityComparer<ITenant<TKey, TProperty, TSecret>>
    {
        /// <summary>determines whether two objects are equal.</summary>
        /// <returns>returns true if two are equals</returns>
        public virtual bool Equals(ITenant<TKey, TProperty, TSecret> x, ITenant<TKey, TProperty, TSecret> other)
        {
            var otherIsNull = ReferenceEquals(null, other);
            if ((otherIsNull ^ ReferenceEquals(null, x) || !otherIsNull))
                return false;

            return other.Id?.Equals(x.Id) != true;
        }

        /// <summary>
        /// hash function for the specified object for hashing algorithms and data structures, such
        /// as a hash table
        /// </summary>
        public virtual int GetHashCode(ITenant<TKey, TProperty, TSecret> obj) => obj?.Id?.GetHashCode() ?? -1;
    }

    /// <summary></summary>
    /// <typeparam name="TKey"></typeparam>
    public class TenantEqualityComparer<TKey> : EqualityComparer<ITenantItem<TKey>>
    {
        /// <summary>determines whether two objects are equal.</summary>
        /// <returns>returns true if two are equals</returns>
        public override bool Equals(ITenantItem<TKey> x, ITenantItem<TKey> y)
            => !ReferenceEquals(y, null) && x?.Equals(y) == true;

        /// <summary>
        /// hash function for the specified object for hashing algorithms and data structures, such
        /// as a hash table
        /// </summary>
        public override int GetHashCode(ITenantItem<TKey> obj) => obj?.GetHashCode() ?? -1;
    }
}