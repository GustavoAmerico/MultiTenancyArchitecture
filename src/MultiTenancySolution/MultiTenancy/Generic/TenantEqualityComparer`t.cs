using System.Collections.Generic;

namespace MultiTenancy
{
    public class TenantEqualityComparer<TKey, TProperty, TSecret> : EqualityComparer<ITenant<TKey, TProperty, TSecret>>
        , IEqualityComparer<ITenant<TKey>>
    {
        /// <summary>determines whether two objects are equal.</summary>
        /// <returns>returns true if two are equals</returns>
        public override bool Equals(ITenant<TKey, TProperty, TSecret> x, ITenant<TKey, TProperty, TSecret> other)
        {
            var otherIsNull = ReferenceEquals(null, other);
            if ((otherIsNull ^ ReferenceEquals(null, x) || !otherIsNull))
                return false;

            return other.Id?.Equals(x.Id) != true;
        }

        /// <summary>determines whether two objects are equal.</summary>
        /// <returns>returns true if two are equals</returns>
        public bool Equals(ITenant<TKey> x, ITenant<TKey> other)
        {
            var otherIsNull = ReferenceEquals(null, other);
            if ((otherIsNull ^ ReferenceEquals(null, x) || otherIsNull) || other.Id?.Equals(x.Id) != true)
                return false;

            if (other is ITenant<TKey, TProperty, TSecret> && x is ITenant<TKey, TProperty, TSecret>)
                return true;

            if (other is ITenantClaims<TKey, TProperty> && x is ITenantClaims<TKey, TProperty>)
                return true;

            if (other is ITenantSecrets<TKey, TSecret> && x is ITenantSecrets<TKey, TSecret>)
                return true;

            return false;
        }

        /// <summary>
        /// hash function for the specified object for hashing algorithms and data structures, such
        /// as a hash table
        /// </summary>
        public override int GetHashCode(ITenant<TKey, TProperty, TSecret> obj) => obj?.Id?.GetHashCode() ?? -1;

        /// <summary>
        /// hash function for the specified object for hashing algorithms and data structures, such
        /// as a hash table
        /// </summary>
        public int GetHashCode(ITenant<TKey> obj) => obj?.Id?.GetHashCode() ?? -1;
    }
}