using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Modules.Catalog.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Inbox;

namespace YetAnotherECommerce.Modules.Catalog.Core.DAL.Postgres;

internal class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<InboxMessage> InboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}