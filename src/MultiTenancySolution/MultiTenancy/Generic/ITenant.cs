using System;

namespace MultiTenancy.Generic
{
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