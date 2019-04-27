using System.Diagnostics.CodeAnalysis;

namespace MultiTenancy.Defaults
{
    public class TenantClaims<TKey, TProperty> : ITenantClaims<TKey, TProperty>
    {
        /// <summary>Gets secrets config for this thenant</summary>
        public virtual TProperty Claims { get; set; }

        public TKey Id { get; protected set; }

        public TenantClaims(TKey id, TProperty claims)
        {
            Claims = claims;
            Id = id;
        }

        protected TenantClaims()
        {
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ITenantClaims<TKey, TProperty>) || Equals(obj as ITenant<TKey>);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        public virtual bool Equals(ITenantClaims<TKey, TProperty> other)
        {
            if (ReferenceEquals(null, other) || other.Id?.Equals(Id) != true)
                return false;

            return other is ITenantClaims<TKey, TProperty>;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(ITenant<TKey> other)
        {
            if (ReferenceEquals(null, other) || other.Id?.Equals(Id) != true)
                return false;

            var areEqualsType = other is ITenantClaims<TKey, TProperty>;
            return areEqualsType;
        }

        /// <summary>Return the has for this object</summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? -1;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{Id} - {Claims}";
        }
    }
}