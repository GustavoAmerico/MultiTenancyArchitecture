using System.Diagnostics.CodeAnalysis;

namespace MultiTenancy.Generic
{
    public class TenantSecret<TKey, TSecret> : TenantItem<TKey>, ITenantSecrets<TKey, TSecret>
    {
        /// <summary>Gets secrets config for this thenant</summary>
        public virtual TSecret Secrets { get; set; }

        public TenantSecret(TKey id, TSecret secrets = default)
        {
            Secrets = secrets;
            Id = id;
        }

        protected TenantSecret()
        {
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        public virtual bool Equals(ITenantSecrets<TKey, TSecret> other)
        {
            if (ReferenceEquals(null, other) || other.Id?.Equals(Id) != true)
                return false;

            return other is ITenantSecrets<TKey, TSecret>;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{Id} - {Secrets}";
        }
    }
}