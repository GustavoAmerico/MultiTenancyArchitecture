using System.Collections.Generic;

namespace MultiTenancy
{
    /// <summary>Lista de tenants na memoria da aplicação</summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TClaims"></typeparam>
    /// <typeparam name="TSecret"></typeparam>
    public interface ITenantCollection<TKey, TClaims, TSecret> : IReadOnlyCollection<ITenant<TKey, TClaims, TSecret>>
    {
        /// <summary>Obtém o tenant com o identificador especificado na consulta</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ITenant<TKey, TClaims, TSecret> FirstOrDefault(TKey id);
    }
}