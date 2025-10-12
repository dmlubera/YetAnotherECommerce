using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Modules.Users.Core.Entities;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Postgres
{
    internal class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}