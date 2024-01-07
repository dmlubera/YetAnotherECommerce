using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Postgres.Configurations
{
    internal class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new Shared.Abstractions.BuildingBlocks.AggregateId(x));
            builder.Property(x => x.FirstName).HasConversion(x => x.Value, x => FirstName.Create(x));
            builder.Property(x => x.LastName).HasConversion(x => x.Value, x => LastName.Create(x));

            builder.OwnsOne(x => x.Address).Property(x => x.Street);
            builder.OwnsOne(x => x.Address).Property(x => x.City);
            builder.OwnsOne(x => x.Address).Property(x => x.ZipCode);
            builder.OwnsOne(x => x.Address).Property(x => x.Country);

        }
    }
}
