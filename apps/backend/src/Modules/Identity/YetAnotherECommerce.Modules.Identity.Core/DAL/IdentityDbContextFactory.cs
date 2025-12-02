using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL;

internal class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
        optionsBuilder
            .UseNpgsql("Host=localhost;Database=yetanotherecommerce;Username=postgres;Password=root",
                options => options.MigrationsHistoryTable("__EFMigrationsHistory", "identity"))
            .UseSeeding((context, _) =>
            {
                if (context.Roles.Any()) return;
                
                var rolesToAdd = new List<IdentityRole<Guid>>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = Role.Admin,
                        NormalizedName = Role.Admin.ToUpperInvariant()
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = Role.Customer,
                        NormalizedName = Role.Customer.ToUpperInvariant()
                    }
                };
                context.Roles.AddRange(rolesToAdd);
                context.SaveChanges();
            });

        return new IdentityDbContext(optionsBuilder.Options);
    }
}