using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres.Repositories
{
    internal class PostgresCustomersRepository : ICustomerRepository
    {
        private readonly OrdersDbContext _dbContext;

        public PostgresCustomersRepository(OrdersDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
            => await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
    }
}
