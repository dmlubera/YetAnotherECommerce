using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.Repositories;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoClient client, IOptions<UsersModuleMongoSettings> settings)
            => _database = client.GetDatabase(settings.Value.DatabaseName);

        public async Task<User> GetByIdAsync(Guid id)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user);

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}