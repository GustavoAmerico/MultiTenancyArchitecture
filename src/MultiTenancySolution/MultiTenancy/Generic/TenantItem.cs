namespace MultiTenancy.Generic
{
    public class TenantItem<TKey> : TenantItem, ITenantItem<TKey>
    {
        private TKey _id;

        public new TKey Id { get => _id; set => base.Id = _id = value; }

        public TenantItem(TKey id) : base(id)
        {
        }

        public TenantItem(ITenantItem<TKey> tenant)
        {
            Clone(tenant);
        }

        public TenantItem()
        {
        }

        public virtual void Clone(ITenantItem<TKey> tenant)
        {
            Id = tenant.Id;
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