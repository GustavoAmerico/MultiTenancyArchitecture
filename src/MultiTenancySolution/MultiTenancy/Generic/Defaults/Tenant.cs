using System;

namespace MultiTenancy.Defaults
{
    /// <summary>This class represents a system tenant instance</summary>
    public class Tenant<TKey, TProperty, TSecret> : TenantBasse<TKey>, ITenant<TKey, TProperty, TSecret>

    {
        private string _name = "";

        /// <summary>Gets the list of configuration for this tenant</summary>
        public virtual TProperty Claims { get; set; }

        /// <summary>obtains a flag indicating whether the tenant is active or not</summary>
        public virtual bool IsEnabled { get; protected set; } = true;

        /// <summary>Gets the public display name from this tenant. </summary>
        public virtual string Name { get => _name; protected set => _name = (value ?? string.Empty).Trim(); }

        /// <summary>Gets secrets config for this thenant</summary>
        public virtual TSecret Secrets { get; set; }

        public Tenant(ITenant<TKey> tenant) : base(tenant.Id)
        {
            if (ReferenceEquals(null, tenant) || ReferenceEquals(null, tenant.Id))
                throw new ArgumentNullException(nameof(tenant), "The tenant key must be fill");

            Id = tenant.Id;
            Merge(tenant);
        }

        public Tenant(TKey id, string name, bool isEnabled = true) : base(id)
        {
            Id = id;
            Name = name;
            IsEnabled = isEnabled;
        }

        public Tenant(TKey id, string name, TProperty claims) : base(id)
        {
            Id = id;
            Name = name;
            Claims = claims;
        }

        public Tenant(TKey id, string name, TSecret secret) : base(id)
        {
            Id = id;
            Name = name;
            Secrets = secret;
        }

        public Tenant(TKey id, string name, TProperty claims, TSecret secret) : base(id)
        {
            Id = id;
            Name = name;
            Claims = claims;
            Secrets = secret;
        }

        public Tenant() : base()
        {
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public virtual bool Equals(ITenant<TKey, TProperty, TSecret> other)
        {
            return !ReferenceEquals(null, other) && other.Id?.Equals(Id) == true;
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public override bool Equals(object obj) => Equals(obj as ITenant<TKey>);

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public override bool Equals(ITenant<TKey> other)
        {
            if (ReferenceEquals(null, other) || other.Id?.Equals(Id) != true)
                return false;

            if (other is ITenant<TKey, TProperty, TSecret> || other is ITenantClaims<TKey, TProperty> || other is ITenantSecrets<TKey, TSecret>)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>Gets the text description</summary>
        public override string ToString()
        {
            return $"{Name} ({Id})";
        }

        protected virtual void Merge(ITenant<TKey> tenant)
        {
            if (tenant is ITenant<TKey, TProperty, TSecret> full)
            {
                Name = full.Name;
                Secrets = full.Secrets;
                IsEnabled = full.IsEnabled;
                Claims = full.Claims;
            }
            else
            {
                if (tenant is ITenantClaims<TKey, TProperty> property)
                    Claims = property.Claims;

                if (tenant is ITenantSecrets<TKey, TSecret> secret)
                    Secrets = secret.Secrets;
            }
        }
    }
}