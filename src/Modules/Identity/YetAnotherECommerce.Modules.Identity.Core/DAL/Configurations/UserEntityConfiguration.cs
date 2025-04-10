using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Role).HasMaxLength(10);
    }
}