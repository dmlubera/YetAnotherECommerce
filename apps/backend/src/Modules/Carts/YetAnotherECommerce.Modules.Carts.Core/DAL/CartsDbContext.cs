using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Inbox;

namespace YetAnotherECommerce.Modules.Carts.Core.DAL;

internal class CartsDbContext(DbContextOptions<CartsDbContext> options) : DbContext(options)
{
    public DbSet<InboxMessage> InboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("carts");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}