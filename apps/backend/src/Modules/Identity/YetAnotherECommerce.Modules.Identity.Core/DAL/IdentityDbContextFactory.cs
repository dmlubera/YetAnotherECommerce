using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL;

internal class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=yetanotherecommerce;Username=postgres;Password=root",
            options => options.MigrationsHistoryTable("__EFMigrationsHistory", "identity"));

        return new IdentityDbContext(optionsBuilder.Options);
    }
}