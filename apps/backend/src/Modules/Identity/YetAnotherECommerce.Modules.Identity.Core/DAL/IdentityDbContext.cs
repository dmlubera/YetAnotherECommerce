using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Outbox;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Identity.IntegrationTests")]
[assembly: InternalsVisibleTo("YetAnotherECommerce.Tests.Acceptance")]
namespace YetAnotherECommerce.Modules.Identity.Core.DAL;

internal class IdentityDbContext(DbContextOptions<IdentityDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}