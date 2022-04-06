using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Identity.Core.Settings
{
    public class IdentityModuleSettings : IModuleSettings
    {
        public string DatabaseName { get; set; }
    }
}