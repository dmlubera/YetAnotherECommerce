using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Postgres;

internal class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=yetanotherecommerce;Username=postgres;Password=root",
            options => options.MigrationsHistoryTable("__EFMigrationsHistory", "users"));

        return new UsersDbContext(optionsBuilder.Options);
    }
}