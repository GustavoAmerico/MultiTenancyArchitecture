using System;
using MultiTenancy.Generic;

namespace MultiTenancy.Hosting.Abstractions
{
    public class TenantScoped<T> : ITenantItem<T>
    {
        private readonly ITenantItem<T> _tenantBrrase;

        T ITenantItem<T>.Id => _tenantBrrase.Id;

        public object Id => throw new NotImplementedException();

        public TenantScoped(ITenantItem<T> tenantBase)
        {
            _tenantBrrase = tenantBase;
        }

        bool IEquatable<ITenantItem<T>>.Equals(ITenantItem<T> other)
        {
            return _tenantBrrase.Equals(other);
        }

        public bool Equals(ITenantItem other)
        {
            throw new NotImplementedException();
        }
    }
}