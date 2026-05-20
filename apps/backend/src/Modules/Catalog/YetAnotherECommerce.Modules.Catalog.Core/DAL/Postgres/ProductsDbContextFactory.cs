using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YetAnotherECommerce.Modules.Catalog.Core.DAL.Postgres;

internal class ProductsDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=yetanotherecommerce;Username=postgres;Password=root",
            options => options.MigrationsHistoryTable("__EFMigrationsHistory", "catalog"));

        return new CatalogDbContext(optionsBuilder.Options);
    }
}