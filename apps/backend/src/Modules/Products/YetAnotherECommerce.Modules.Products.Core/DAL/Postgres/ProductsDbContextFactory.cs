using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Postgres;

internal class ProductsDbContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
{
    public ProductsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=yetanotherecommerce;Username=postgres;Password=root",
            options => options.MigrationsHistoryTable("__EFMigrationsHistory", "products"));

        return new ProductsDbContext(optionsBuilder.Options);
    }
}