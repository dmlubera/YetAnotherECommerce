using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres
{
    internal class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("orders");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

    }
}
