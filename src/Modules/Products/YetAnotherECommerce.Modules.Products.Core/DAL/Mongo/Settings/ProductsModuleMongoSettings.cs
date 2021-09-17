using YetAnotherECommerce.Shared.Abstractions.Mongo;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Settings
{
    public class ProductsModuleMongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}