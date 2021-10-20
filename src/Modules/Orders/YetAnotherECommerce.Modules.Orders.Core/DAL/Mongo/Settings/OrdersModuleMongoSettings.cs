using YetAnotherECommerce.Shared.Abstractions.Mongo;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Settings
{
    public class OrdersModuleMongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}