using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync();
        Task AddAsync(Product product);
        Task<bool> CheckIfProductAlreadyExistsAsync(string name);
    }
}