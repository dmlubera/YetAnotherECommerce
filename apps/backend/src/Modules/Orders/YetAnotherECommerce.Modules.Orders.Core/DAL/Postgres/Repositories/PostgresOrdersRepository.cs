using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres.Repositories;

internal class PostgresOrdersRepository(OrdersDbContext dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Order>> BrowseAsync()
        => await dbContext.Orders
            .Include(x => x.OrderItems)
            .ToListAsync();

    public async Task<IReadOnlyList<Order>> BrowseByCustomerAsync(Guid customerId)
        => await dbContext.Orders
            .Include(x => x.OrderItems)
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();

    public async Task<Order> GetByIdAsync(Guid id)
        => await dbContext.Orders
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Order> GetForCustomerByIdAsync(Guid customerId, Guid orderId)
    {
        //TODO: Do I really need it?
        return await dbContext.Orders
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Id == orderId);
    }

    public async Task UpdateAsync(Order order)
    {
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync();
    }
}