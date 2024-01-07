using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Postgres.Repositories
{
    internal class PostgresUserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;

        public PostgresUserRepository(UsersDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddAsync(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
            => await _dbContext.Users.FindAsync(new AggregateId(id));

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
