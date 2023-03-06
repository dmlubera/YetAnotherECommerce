using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Products.Core.Settings
{
    public class ProductsModuleSettings : IModuleSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}