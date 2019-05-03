using System.Diagnostics.CodeAnalysis;

namespace MultiTenancy.Generic.Defaults
{
    public class TenantSecret<TKey, TSecret> : ITenantSecrets<TKey, TSecret>
        where TSecret : new()
    {
        /// <summary>Gets the public identifier from this tenant</summary>
        public TKey Id { get; protected set; }

        object ITenantItem.Id => Id;

        /// <summary>Gets secrets config for this thenant</summary>
        public virtual TSecret Secrets { get; set; } = new TSecret();

        public TenantSecret(TKey id, TSecret secrets = default)
        {
            Secrets = ReferenceEquals(null, secrets) ? new TSecret() : secrets;
            Id = id;
        }

        protected TenantSecret()
        {
            Secrets = new TSecret();
        }

        public bool Equals(ITenantItem other)
        {
            return Equals((object)other) || other?.Id.Equals(this.Id) == true;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ITenantSecrets<TKey, TSecret>) || Equals(obj as ITenantItem<TKey>);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        public virtual bool Equals(ITenantSecrets<TKey, TSecret> other)
        {
            if (ReferenceEquals(null, other) || other.Id?.Equals(Id) != true)
                return false;

            return other is ITenantSecrets<TKey, TSecret>;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(ITenantItem<TKey> other)
        {
            if (ReferenceEquals(null, other) || other.Id?.Equals(Id) != true)
                return false;

            return other is ITenantSecrets<TKey, TSecret>;
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
            return $"{Id} - {Secrets}";
        }
    }
}