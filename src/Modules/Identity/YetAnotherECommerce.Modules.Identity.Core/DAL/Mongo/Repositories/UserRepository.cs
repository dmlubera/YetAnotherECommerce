using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoClient client, IOptions<IdentityModuleMongoSettings> settings)
        {
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user);

        public async Task<User> GetByEmailAsync(string email)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email.Value == email);

        public async Task<User> GetByIdAsync(Guid id)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> CheckIfEmailIsInUseAsync(string email)
            => await Users.AsQueryable().AnyAsync(x => x.Email.Value == email);

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}