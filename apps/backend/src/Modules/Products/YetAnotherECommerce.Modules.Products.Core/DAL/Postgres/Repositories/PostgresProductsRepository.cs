using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Postgres.Repositories;

internal class PostgresProductsRepository(ProductsDbContext dbContext) : IProductRepository
{
    public async Task AddAsync(Product product)
    {
        dbContext.Add(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> CheckIfProductAlreadyExistsAsync(string name)
        => await dbContext.Products.AnyAsync(x => x.Name == name);

    public async Task DeleteAsync(Guid id)
    {
        dbContext.Remove(id);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Product>> GetAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await dbContext.Products.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        dbContext.Update(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(IEnumerable<Product> products)
    {
        dbContext.UpdateRange(products);
        await dbContext.SaveChangesAsync();
    }
}