using System.Diagnostics.CodeAnalysis;

namespace MultiTenancy.Generic
{
    public class TenantClaims<TKey, TProperty> : TenantItem<TKey>, ITenantClaims<TKey, TProperty>
    {
        /// <summary>Gets secrets config for this thenant</summary>
        public virtual TProperty Claims { get; set; }

        public TenantClaims(TKey id, TProperty claims)
        {
            Claims = claims;
            Id = id;
        }

        protected TenantClaims()
        {
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        public virtual bool Equals(ITenantClaims<TKey, TProperty> other)
        {
            if (other?.Id?.Equals(Id) != true)
                return false;

            return other is ITenantClaims<TKey, TProperty>;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{Id} - {Claims}";
        }
    }
}