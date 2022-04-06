using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.Core.Settings;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;

        public ProductRepository(IMongoClient client, IOptions<ProductsModuleSettings> settings)
        {
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var document = await Products.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            return document?.AsEntity();
        }

        public async Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var documents = await Products.Find(x => ids.Contains(x.Id)).ToListAsync();
            return documents.Select(x => x.AsEntity()).ToList();
        }

        public async Task<IReadOnlyList<Product>> GetAsync()
        {
            var documents = await Products.Find(x => true).ToListAsync();

            return documents.Select(x => x.AsEntity()).ToList();
        }

        public async Task AddAsync(Product product)
            => await Products.InsertOneAsync(product.AsDocument());

        public async Task<bool> CheckIfProductAlreadyExistsAsync(string name)
            => await Products.AsQueryable().AnyAsync(x => x.Name == name);

        public async Task DeleteAsync(Guid id)
            => await Products.DeleteOneAsync(x => x.Id == id);

        public async Task UpdateAsync(Product product)
            => await Products.ReplaceOneAsync(x => x.Id == product.Id, product.AsDocument());

        public async Task UpdateAsync(IEnumerable<Product> products)
        {
            var updates = new List<WriteModel<ProductDocument>>();
            foreach(var product in products)
            {
                var document = product.AsDocument();
                var filter = Builders<ProductDocument>.Filter.Eq(x => x.Id, document.Id);
                updates.Add(new ReplaceOneModel<ProductDocument>(filter, document));
            }

            await Products.BulkWriteAsync(updates);
        }

        private IMongoCollection<ProductDocument> Products => _database.GetCollection<ProductDocument>("Products");
    }
}