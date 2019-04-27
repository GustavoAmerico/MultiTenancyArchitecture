namespace MultiTenancy.Defaults
{
    public class Tenant<TKey> : ITenant<TKey>
    {
        /// <summary>Gets the public identifier from this tenant</summary>
        public TKey Id { get; protected set; }

        public Tenant()
        {
        }

        public Tenant(TKey id)
        {
            Id = id;
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public virtual bool Equals(ITenant<TKey> other)
        {
            if (ReferenceEquals(null, other) || other.Id?.Equals(Id) != true)
                return false;

            return true;
        }
    }
}