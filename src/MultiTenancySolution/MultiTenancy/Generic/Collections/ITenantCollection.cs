using System.Collections.Generic;

namespace MultiTenancy
{
    public interface ITenantCollection<TKey, TProperty, TSecret> : IReadOnlyCollection<ITenant<TKey, TProperty, TSecret>>
    {
    }
}