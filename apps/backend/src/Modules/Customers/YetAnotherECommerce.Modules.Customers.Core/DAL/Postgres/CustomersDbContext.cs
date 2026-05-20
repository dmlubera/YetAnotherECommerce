using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Modules.Customers.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Inbox;

namespace YetAnotherECommerce.Modules.Customers.Core.DAL.Postgres;

internal class CustomersDbContext(DbContextOptions<CustomersDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<InboxMessage> InboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("customers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}