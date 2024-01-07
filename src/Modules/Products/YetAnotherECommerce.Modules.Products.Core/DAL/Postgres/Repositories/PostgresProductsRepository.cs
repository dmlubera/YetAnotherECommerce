using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Postgres.Repositories
{
    internal class PostgresProductsRepository : IProductRepository
    {
        private readonly ProductsDbContext _dbContext;

        public PostgresProductsRepository(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Product product)
        {
            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfProductAlreadyExistsAsync(string name)
            => await _dbContext.Products.AnyAsync(x => x.Name == name);

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Remove(id);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _dbContext.Products.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<Product> products)
        {
            _dbContext.UpdateRange(products);
            await _dbContext.SaveChangesAsync();
        }
    }
}
