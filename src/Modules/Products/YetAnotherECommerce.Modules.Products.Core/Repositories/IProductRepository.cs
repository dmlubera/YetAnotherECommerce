using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.Core.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<bool> CheckIfProductAlreadyExistsAsync(string name);
    }
}