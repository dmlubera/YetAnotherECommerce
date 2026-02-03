DB_CONNECTION_STRING='Host=localhost;Port=5432;Database=yetanotherecommerce;Username=postgres;Password=root'

echo 'Creating directories...'
mkdir -p migrations/Identity migrations/Users migrations/Products migrations/Orders migrations/Carts

echo 'Building solution...'
dotnet build ./apps/backend --no-restore

echo 'Creating migrations bundles...'
dotnet ef migrations bundle --no-build --force \
--project ./apps/backend/src/Modules/Identity/YetAnotherECommerce.Modules.Identity.Core/YetAnotherECommerce.Modules.Identity.Core.csproj \
--context IdentityDbContext \
--output migrations/Identity/migrate

dotnet ef migrations bundle --no-build --force \
--project ./apps/backend/src/Modules/Users/YetAnotherECommerce.Modules.Users.Core/YetAnotherECommerce.Modules.Users.Core.csproj \
--context UsersDbContext \
--output migrations/Users/migrate

dotnet ef migrations bundle --no-build --force \
--project ./apps/backend/src/Modules/Products/YetAnotherECommerce.Modules.Products.Core/YetAnotherECommerce.Modules.Products.Core.csproj \
--context ProductsDbContext \
--output migrations/Products/migrate

dotnet ef migrations bundle --no-build --force \
--project ./apps/backend/src/Modules/Orders/YetAnotherECommerce.Modules.Orders.Core/YetAnotherECommerce.Modules.Orders.Core.csproj \
--context OrdersDbContext \
--output migrations/Orders/migrate

dotnet ef migrations bundle --no-build --force \
--project ./apps/backend/src/Modules/Carts/YetAnotherECommerce.Modules.Carts.Core/YetAnotherECommerce.Modules.Carts.Core.csproj \
--context CartsDbContext \
--output migrations/Carts/migrate

echo 'Applying migrations...'
migrations/Identity/migrate --connection $DB_CONNECTION_STRING
migrations/Users/migrate --connection $DB_CONNECTION_STRING
migrations/Products/migrate --connection $DB_CONNECTION_STRING
migrations/Orders/migrate --connection $DB_CONNECTION_STRING
migrations/Carts/migrate --connection $DB_CONNECTION_STRING

echo 'Cleaning up...'
rm -r migrations