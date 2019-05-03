namespace MultiTenancy.Generic
{
    /// <summary>This class represents a tenant abstraction of the system with configuration</summary>
    /// <typeparam name="TKey">The key of the tenant</typeparam>
    /// <typeparam name="TProperty">The extra configuration</typeparam>
    public interface ITenantClaims<TKey, TProperty> : ITenantItem<TKey>

    {
        /// <summary>Gets and send settings of the tenant</summary>
        TProperty Claims { get; set; }
    }
}