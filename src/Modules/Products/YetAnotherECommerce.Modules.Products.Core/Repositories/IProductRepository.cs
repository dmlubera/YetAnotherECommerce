using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task<bool> CheckIfProductAlreadyExistsAsync(string name);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Product product);
    }
}