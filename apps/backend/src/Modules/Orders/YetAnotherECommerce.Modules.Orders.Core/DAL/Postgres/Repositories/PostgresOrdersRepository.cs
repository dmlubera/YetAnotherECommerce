using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres.Repositories
{
    internal class PostgresOrdersRepository : IOrderRepository
    {
        private readonly OrdersDbContext _dbContext;

        public PostgresOrdersRepository(OrdersDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Order>> BrowseAsync()
            => await _dbContext.Orders
            .Include(x => x.OrderItems)
            .ToListAsync();

        public async Task<IReadOnlyList<Order>> BrowseByCustomerAsync(Guid customerId)
            => await _dbContext.Orders
            .Include(x => x.OrderItems)
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();

        public async Task<Order> GetByIdAsync(Guid id)
            => await _dbContext.Orders
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Order> GetForCustomerByIdAsync(Guid customerId, Guid orderId)
        {
            //TODO: Do I really need it?
            return await _dbContext.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Id == orderId);
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
