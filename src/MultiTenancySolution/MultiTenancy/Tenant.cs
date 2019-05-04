namespace MultiTenancy
{
    public class Tenant : TenantItem, ITenant
    {
        private string _name;
        public bool IsEnabled { get; protected set; }

        public string Name
        {
            get => _name;
            protected set => _name = (value ?? string.Empty).Trim();
        }
    }
}