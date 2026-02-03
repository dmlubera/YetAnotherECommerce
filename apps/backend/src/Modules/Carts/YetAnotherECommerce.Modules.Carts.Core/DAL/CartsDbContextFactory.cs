using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YetAnotherECommerce.Modules.Carts.Core.DAL;

internal class CartsDbContextFactory : IDesignTimeDbContextFactory<CartsDbContext>
{
    public CartsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CartsDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=yetanotherecommerce;Username=postgres;Password=root",
            options => options.MigrationsHistoryTable("__EFMigrationsHistory", "carts"));

        return new CartsDbContext(optionsBuilder.Options);
    }
}