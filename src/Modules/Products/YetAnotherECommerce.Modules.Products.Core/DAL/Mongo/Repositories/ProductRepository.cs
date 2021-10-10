using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;

        public ProductRepository(IMongoClient client, IOptions<ProductsModuleMongoSettings> settings)
        {
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var document = await Products.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            return document.AsEntity();
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            var documents = await Products.Find(x => true).ToListAsync();

            return documents.Select(x => x.AsEntity());
        }

        public async Task AddAsync(Product product)
            => await Products.InsertOneAsync(product.AsDocument());

        public async Task<bool> CheckIfProductAlreadyExistsAsync(string name)
            => await Products.AsQueryable().AnyAsync(x => x.Name == name);

        public async Task DeleteAsync(Guid id)
            => await Products.DeleteOneAsync(x => x.Id == id);

        public async Task UpdateAsync(Product product)
            => await Products.InsertOneAsync(product.AsDocument());

        private IMongoCollection<ProductDocument> Products => _database.GetCollection<ProductDocument>("Products");
    }
}