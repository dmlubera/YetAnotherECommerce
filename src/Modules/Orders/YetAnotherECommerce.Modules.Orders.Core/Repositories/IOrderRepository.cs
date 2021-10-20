using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
    }
}