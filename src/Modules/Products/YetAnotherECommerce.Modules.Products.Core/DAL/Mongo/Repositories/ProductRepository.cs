using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            => await Products.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Product>> GetAsync()
            => await Products.Find(x => true).ToListAsync();

        public async Task AddAsync(Product product)
            => await Products.InsertOneAsync(product);

        public async Task<bool> CheckIfProductAlreadyExistsAsync(string name)
            => await Products.AsQueryable().AnyAsync(x => x.Name == name);

        private IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    }
}