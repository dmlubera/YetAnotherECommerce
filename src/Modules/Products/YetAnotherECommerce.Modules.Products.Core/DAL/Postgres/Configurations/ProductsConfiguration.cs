using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Postgres.Configurations
{
    internal class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new AggregateId(x));
            builder.Property(x => x.Name).HasConversion(x => x.Value, x => Name.Create(x));
            builder.Property(x => x.Price).HasConversion(x => x.Value, x => Price.Create(x));
            builder.Property(x => x.Quantity).HasConversion(x => x.Value, x => Quantity.Create(x));
        }
    }
}
