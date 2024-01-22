using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Identity.IntegrationTests")]
[assembly: InternalsVisibleTo("YetAnotherECommerce.Tests.Acceptance")]
namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres
{
    internal class IdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("identity");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
