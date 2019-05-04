using System.Diagnostics.CodeAnalysis;

namespace MultiTenancy
{
    public class TenantItem : ITenantItem
    {
        public virtual object Id { get; protected set; }

        public TenantItem()
        {
        }

        public TenantItem(object id)
        {
            Id = id;
        }

        public static bool operator !=(TenantItem left, TenantItem right) => !Equals(left, right);

        /// <summary>Return equals</summary>
        /// <param name="left"> </param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(TenantItem left, TenantItem right) => Equals(left, right);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ITenantItem);
        }

        [ExcludeFromCodeCoverage]
        public virtual bool Equals(TenantItem other) => Equals(Id, other?.Id);

        [ExcludeFromCodeCoverage]
        public virtual bool Equals(ITenantItem other) => Equals(Id, other?.Id);

        [ExcludeFromCodeCoverage]
        public override int GetHashCode() => (Id != null ? Id.GetHashCode() : 0);
    }
}