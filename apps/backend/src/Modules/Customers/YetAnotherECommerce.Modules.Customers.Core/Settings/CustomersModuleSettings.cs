using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Customers.Core.Settings;

public class CustomersModuleSettings : IModuleSettings
{
    public string CollectionName { get; set; }
    public string DatabaseName { get; set; }
}