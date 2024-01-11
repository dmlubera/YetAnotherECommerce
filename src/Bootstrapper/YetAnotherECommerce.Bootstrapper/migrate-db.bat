::Identity Module
dotnet ef database update -c IdentityDbContext -p ..\..\Modules\Identity\YetAnotherECommerce.Modules.Identity.Core

::Users Module
dotnet ef database update -c UsersDbContext -p ..\..\Modules\Users\YetAnotherECommerce.Modules.Users.Core

::Products Module
dotnet ef database update -c ProductsDbContext -p ..\..\Modules\Products\YetAnotherECommerce.Modules.Products.Core

::Orders Module
dotnet ef database update -c OrdersDbContext -p ..\..\Modules\Orders\YetAnotherECommerce.Modules.Orders.Core