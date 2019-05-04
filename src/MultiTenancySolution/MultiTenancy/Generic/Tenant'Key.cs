namespace MultiTenancy.Generic
{
    public class Tenant<TKey> : ITenantItem<TKey>, ITenant
    {
        private string _name = string.Empty;

        /// <summary>Gets the public identifier from this tenant</summary>
        public virtual TKey Id { get; protected set; }

        object ITenantItem.Id => Id;

        /// <summary>obtains a flag indicating whether the tenant is active or not</summary>
        public virtual bool IsEnabled { get; protected set; } = true;

        /// <summary>Gets the public display name from this tenant.</summary>
        public virtual string Name { get => _name; protected set => _name = (value ?? string.Empty).Trim(); }

        public Tenant()
        {
        }

        public Tenant(TKey id, string name = "", bool isEnabled = true)
        {
            Id = id;
            Name = name;
            IsEnabled = isEnabled;
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public virtual bool Equals(ITenantItem<TKey> other)
        {
            if (ReferenceEquals(null, other) || other.Id?.Equals(Id) != true)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ITenantItem<TKey>);
        }

        public bool Equals(ITenantItem other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (other is ITenantItem<TKey> okey)
                return okey.Equals(this);

            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}