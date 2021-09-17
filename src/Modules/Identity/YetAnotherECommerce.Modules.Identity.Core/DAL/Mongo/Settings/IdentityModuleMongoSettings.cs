using YetAnotherECommerce.Shared.Abstractions.Mongo;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings
{
    public class IdentityModuleMongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}