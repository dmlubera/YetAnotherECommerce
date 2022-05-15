using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Modules.Orders.Core.Settings;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public CustomerRepository(IMongoClient client, IOptions<OrdersModuleSettings> settings)
            => _mongoDatabase = client.GetDatabase(settings.Value.DatabaseName);

        public async Task<Customer> GetByIdAsync(Guid id)
            => await Customers.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Customer customer)
            => await Customers.InsertOneAsync(customer);

        private IMongoCollection<Customer> Customers => _mongoDatabase.GetCollection<Customer>("Customers");
    }
}