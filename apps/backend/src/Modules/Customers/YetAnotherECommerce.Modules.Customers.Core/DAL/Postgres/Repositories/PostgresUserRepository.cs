using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Customers.Core.Entities;
using YetAnotherECommerce.Modules.Customers.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Customers.Core.DAL.Postgres.Repositories;

internal class PostgresUserRepository(CustomersDbContext dbContext) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        dbContext.Add(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
        => await dbContext.Users.FindAsync(new AggregateId(id));

    public async Task UpdateAsync(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }
}