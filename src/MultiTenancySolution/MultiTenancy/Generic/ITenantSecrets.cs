namespace MultiTenancy
{
    public interface ITenantSecrets<TKey, TSecret> : ITenant<TKey>
    {
        /// <summary>Gets the list of configuration for this tenant</summary>
        TSecret Secrets { get; set; }
    }
}