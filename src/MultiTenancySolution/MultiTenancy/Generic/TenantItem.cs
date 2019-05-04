namespace MultiTenancy.Generic
{
    public class TenantItem<TKey> : TenantItem, ITenantItem<TKey>
    {
        public new TKey Id { get; set; }

        public TenantItem(TKey id) : base(id)
        {
        }

        public TenantItem()
        {
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        public bool Equals(ITenantItem<TKey> other)
        {
            return this.Equals((ITenantItem)other);
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}