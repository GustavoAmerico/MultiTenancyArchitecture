using System;
using System.Collections.Generic;
using MultiTenancy.Generic;

namespace MultiTenancy.Providers
{
    /// <summary>Defines the mechanism for retrieving all tenants for application</summary>
    public interface ITenantProvider
    {
        /// <summary>Retrieving all tenants for application</summary>
        /// <typeparam name="TKey">This is the client identifier</typeparam>
        /// <typeparam name="TProperty">This is the type of custom claims</typeparam>
        /// <typeparam name="TSecret">This is the type of custom secrets</typeparam>
        /// <param name="service">Dechanism for retrieving a service object</param>
        HashSet<ITenantItem<TKey>> GetTenants<TKey>();
    }
}