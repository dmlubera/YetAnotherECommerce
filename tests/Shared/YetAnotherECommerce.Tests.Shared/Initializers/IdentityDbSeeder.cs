using MongoDB.Driver;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Tests.Shared.Initializers
{
    public class IdentityDbSeeder : IMongoDbSeeder
    {
        public async Task Seed(IMongoDatabase database)
        {
            var encrypter = new Encrypter();
            var salt = encrypter.GetSalt();
            var hash = encrypter.GetHash("super$ecret", salt);
            var password = Password.Create(hash, salt);
            var usersCollection = database.GetCollection<UserDocument>("Users");
            var customer = User.Create(Email.Create("customer@yetanotherecommerce.com"), password, "customer");
            var admin = User.Create(Email.Create("admin@yetanotherecommerce.com"), password, "admin");

            await usersCollection.InsertManyAsync(new []{ customer.AsDocument(), admin.AsDocument() });
        }
    }
}