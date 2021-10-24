using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public CustomerRepository(IMongoClient client, IOptions<OrdersModuleMongoSettings> settings)
            => _mongoDatabase = client.GetDatabase(settings.Value.DatabaseName);

        public async Task AddAsync(Customer customer)
            => await Customers.InsertOneAsync(customer);

        private IMongoCollection<Customer> Customers => _mongoDatabase.GetCollection<Customer>("Customers");
    }
}