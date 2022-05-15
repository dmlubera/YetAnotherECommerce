using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Modules.Users.Core.Settings;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoClient client, IOptions<UsersModuleSettings> settings)
            => _database = client.GetDatabase(settings.Value.DatabaseName);

        public async Task<User> GetByIdAsync(Guid id)
        {
            var document = await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            return document.AsEntity();
        }

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user.AsDocument());

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync(x => x.Id == user.Id, user.AsDocument());

        private IMongoCollection<UserDocument> Users => _database.GetCollection<UserDocument>("Users");
    }
}