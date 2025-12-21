using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.Core.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task AddAsync(Product product);
    Task<bool> CheckIfProductAlreadyExistsAsync(string name);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Product product);
    Task UpdateAsync(IEnumerable<Product> products);
}