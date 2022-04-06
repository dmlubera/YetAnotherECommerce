using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Orders.Core.Settings
{
    public class OrdersModuleSettings : IModuleSettings
    {
        public string DatabaseName { get; set; }
    }
}