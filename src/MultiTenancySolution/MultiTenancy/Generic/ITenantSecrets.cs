namespace MultiTenancy.Generic
{
    public interface ITenantSecrets<TKey, TSecret> : ITenantItem<TKey>
    {
        /// <summary>Gets the list of configuration for this tenant</summary>
        TSecret Secrets { get; set; }
    }
}