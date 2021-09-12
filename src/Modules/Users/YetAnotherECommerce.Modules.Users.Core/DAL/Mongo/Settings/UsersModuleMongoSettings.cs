using YetAnotherECommerce.Shared.Abstractions.Mongo;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Settings
{
    public class UsersModuleMongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}