using System;

namespace MultiTenancy
{
    /// <summary>
    /// This class represents a tenant abstraction of the system with custum configs and secrets
    /// </summary>
    public interface ITenant<TKey> : IEquatable<ITenant<TKey>>
    {
        /// <summary>Gets the public identifier from this tenant</summary>
        TKey Id { get; }
    }

    /// <summary>
    /// This class represents a tenant abstraction of the system with custum configs and secrets
    /// </summary>
    /// <typeparam name="TKey">Type of the identifier</typeparam>
    public interface ITenant<TKey, TProperty, TSecret>
        : ITenantClaims<TKey, TProperty>
        , ITenantSecrets<TKey, TSecret>
        , IEquatable<ITenant<TKey, TProperty, TSecret>>
        , ITenant
    {
    }
}