using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres.Repositories;

internal class PostgresCustomersRepository(OrdersDbContext dbContext) : ICustomerRepository
{
    public async Task AddAsync(Customer customer)
    {
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Customer> GetByIdAsync(Guid id)
        => await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
}