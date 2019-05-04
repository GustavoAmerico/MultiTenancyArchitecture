using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiTenancy.Generic
{
    /// <summary>This class represents a system tenant instance</summary>
    public class Tenant<TKey, TProperty, TSecret> : TenantItem<TKey>, ITenant<TKey, TProperty, TSecret>

    {
        private string _name = "";

        /// <summary>Gets the list of configuration for this tenant</summary>
        public virtual TProperty Claims { get; set; }

        public bool IsEnabled { get; protected set; }

        public string Name { get => _name; protected set => _name = (value ?? string.Empty).Trim(); }

        /// <summary>Gets secrets config for this thenant</summary>
        public virtual TSecret Secrets { get; set; }

        /// <summary>Initialize an object with default value based on <paramref name="tenant"/></summary>
        /// <param name="tenant">The tenant base for initialize properties values</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="tenant"/> is null.</exception>
        public Tenant(ITenantItem<TKey> tenant)
        {
            if (ReferenceEquals(null, tenant) || ReferenceEquals(null, tenant.Id))
                throw new ArgumentNullException(nameof(tenant), "The tenant key must be fill");

            Id = tenant.Id;
            Merge(tenant);
        }

        public Tenant(TKey id, string name, bool isEnabled = true)
        {
            Id = id;
            Name = name;
            IsEnabled = isEnabled;
        }

        public Tenant(TKey id, string name, TProperty claims) : this(id, name)
        {
            Claims = claims;
        }

        public Tenant(TKey id, string name, TSecret secret) : this(id, name)
        {
            Secrets = secret;
        }

        public Tenant(TKey id, string name, TProperty claims, TSecret secret) : this(id, name)
        {
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

        /// <summary>Gets the text description</summary>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{Name} ({Id})";
        }

        protected virtual void Merge(ITenantItem<TKey> tenant)
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