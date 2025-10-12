using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Postgres
{
    internal class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("products");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
