using System.Collections.Generic;
using MultiTenancy.Generic;

namespace MultiTenancy.Collections
{
    /// <summary>Represents the list of clients associated with the application</summary>
    public interface ITenantCollection : IReadOnlyCollection<ITenantItem>
    {
        /// <summary>Obtém o tenant com o identificador especificado na consulta</summary>
        /// <param name="id"></param>
        ITenantItem<TKey> FirstOrDefault<TKey>(TKey id);

        /// <summary>Obtém o tenant com o identificador especificado na consulta</summary>
        /// <param name="id"></param>
        ITenant<TKey, TClaims, TSecret> FirstOrDefault<TKey, TClaims, TSecret>(TKey id);
    }
}