using System;
using System.Collections.Generic;

namespace MultiTenancy.Providers
{
    /// <summary>Defines how the custom claims of each client will be loaded</summary>
    public interface ITenantClaimsProvider
    {
        /// <summary>Gets all custum fields for all cients</summary>
        /// <typeparam name="TKey">This is the client identifier</typeparam>
        /// <typeparam name="TProperty">This is the type of custom claims</typeparam>
        /// <param name="service">Dechanism for retrieving a service object</param>
        HashSet<ITenantClaims<TKey, TProperty>> GetTenantClaims<TKey, TProperty>();
    }
}