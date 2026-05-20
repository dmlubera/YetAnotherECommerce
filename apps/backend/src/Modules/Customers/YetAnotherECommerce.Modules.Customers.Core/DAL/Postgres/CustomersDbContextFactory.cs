using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YetAnotherECommerce.Modules.Customers.Core.DAL.Postgres;

internal class CustomersDbContextFactory : IDesignTimeDbContextFactory<CustomersDbContext>
{
    public CustomersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CustomersDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=yetanotherecommerce;Username=postgres;Password=root",
            options => options.MigrationsHistoryTable("__EFMigrationsHistory", "customers"));

        return new CustomersDbContext(optionsBuilder.Options);
    }
}