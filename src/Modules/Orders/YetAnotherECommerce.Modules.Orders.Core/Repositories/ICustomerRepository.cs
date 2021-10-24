using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
    }
}