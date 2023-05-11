using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres.Configurations
{
    internal class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new AggregateId(x));
            builder.Property(x => x.Email).HasConversion(x => x.Value, x => new ValueObjects.Email(x));

            builder.OwnsOne(x => x.Password).Property(x => x.Hash);
            builder.OwnsOne(x => x.Password).Property(x => x.Salt);
        }
    }
}
