using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Configurations;

public class UserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.HasData(
            new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = Role.Admin,
                NormalizedName = Role.Admin.ToUpperInvariant(),
            },
            new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = Role.Customer,
                NormalizedName = Role.Customer.ToUpperInvariant(),
            }
        );
    }
}