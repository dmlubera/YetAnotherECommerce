using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Catalog.Core.Settings;

public class CatalogModuleSettings : IModuleSettings
{
    public string CollectionName { get; set; }
    public string DatabaseName { get; set; }
}