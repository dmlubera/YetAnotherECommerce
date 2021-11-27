using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public OrderRepository(IMongoClient mongoClient, IOptions<OrdersModuleMongoSettings> settings)
            => _mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

        public async Task<IList<Order>> BrowseAsync()
        {
            var documents = await Orders.Find(x => true).ToListAsync();

            return documents.Select(x => x.AsEntity()).ToList();
        }

        public async Task<IList<Order>> BrowseByCustomerAsync(Guid customerId)
        {
            var documents = await Orders.Find(x => x.CustomerId == customerId).ToListAsync();

            return documents.Select(x => x.AsEntity()).ToList();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var document = await Orders.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            return document.AsEntity();
        }

        public async Task<Order> GetForCustomerByIdAsync(Guid customerId, Guid orderId)
        {
            var document = await Orders.AsQueryable().FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Id == orderId);

            return document.AsEntity();
        }

        public async Task AddAsync(Order order)
            => await Orders.InsertOneAsync(order.AsDocument());

        public async Task UpdateAsync(Order order)
            => await Orders.ReplaceOneAsync(x => x.Id == order.Id, order.AsDocument());

        private IMongoCollection<OrderDocument> Orders => _mongoDatabase.GetCollection<OrderDocument>("Orders");
    }
}