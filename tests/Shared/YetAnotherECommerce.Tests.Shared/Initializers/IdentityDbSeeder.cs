using MongoDB.Driver;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Tests.Shared.Initializers
{
    public class IdentityDbSeeder : IMongoDbSeeder
    {
        public async Task Seed(IMongoDatabase database)
        {
            var usersCollection = database.GetCollection<UserDocument>("Users");
            var customer = new User("customer@yetanotherecommerce.com", "super$ecret", "customer");
            var admin = new User("admin@yetanotherecommerce.com", "super$ecret", "admin");

            await usersCollection.InsertManyAsync(new []{ customer.AsDocument(), admin.AsDocument() });
        }
    }
}