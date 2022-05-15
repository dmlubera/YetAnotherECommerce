using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents;

namespace YetAnotherECommerce.Tests.Shared.Initializers
{
    public class ProductsDbSeeder : IMongoDbSeeder
    {
        public async Task Seed(IMongoDatabase database)
        {
            var products = database.GetCollection<ProductDocument>("Products");
            var product = new ProductDocument
            {
                Id = Guid.NewGuid(),
                Name = "Existed product",
                Description = string.Empty,
                Price = 100,
                Quantity = 10
            };

            await products.InsertOneAsync(product);
        }
    }
}