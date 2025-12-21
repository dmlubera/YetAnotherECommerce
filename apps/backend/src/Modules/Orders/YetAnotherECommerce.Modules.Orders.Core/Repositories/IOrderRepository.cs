using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.Repositories;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> BrowseAsync();
    Task<IReadOnlyList<Order>> BrowseByCustomerAsync(Guid customerId);
    Task<Order> GetByIdAsync(Guid id);
    Task<Order> GetForCustomerByIdAsync(Guid customerId, Guid orderId);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}