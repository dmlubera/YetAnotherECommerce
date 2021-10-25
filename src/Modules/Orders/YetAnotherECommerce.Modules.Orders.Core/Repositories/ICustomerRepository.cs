using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid id);
        Task AddAsync(Customer customer);
    }
}