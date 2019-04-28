using System;
using System.Collections.Generic;

namespace MultiTenancy.Providers
{
    /// <summary>Defines how the custom secrets of each client will be loaded</summary>
    public interface ITenantSecretProvider
    {
        /// <summary>Gets all custum secrets for all cients</summary>
        /// <typeparam name="TKey">This is the client identifier</typeparam>
        /// <typeparam name="TSecret">This is the type of custom secret</typeparam>
        /// <param name="service">Dechanism for retrieving a service object</param>
        HashSet<ITenantSecrets<TKey, TSecret>> GetTenantSecrets<TKey, TSecret>();
    }
}