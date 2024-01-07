using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres.Repositories
{
    internal class PostgresUserRepository : IUserRepository
    {
        private readonly IdentityDbContext _dbContext;

        public PostgresUserRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfEmailIsInUseAsync(string email)
            => await _dbContext.Users.AnyAsync(x => x.Email == email);

        public async Task<User> GetByEmailAsync(string email)
            => await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetByIdAsync(Guid id)
            => await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
