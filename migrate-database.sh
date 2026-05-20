DB_CONNECTION_STRING='Host=localhost;Port=5432;Database=yetanotherecommerce;Username=postgres;Password=root'

echo 'Creating directories...'
mkdir -p migrations/Identity migrations/Customers migrations/Catalog migrations/Orders migrations/Carts

echo 'Building solution...'
dotnet build ./apps/backend --no-restore

echo 'Creating migrations bundles...'
dotnet ef migrations bundle --no-build --force \
--project ./apps/backend/src/Modules/Identity/YetAnotherECommerce.Modules.Identity.Core/YetAnotherECommerce.Modules.Identity.Core.csproj \
--context IdentityDbContext \
--output migrations/Identity/migrate

dotnet ef migrations bundle --no-build --force \
--project ./apps/backend/src/Modules/Customers/YetAnotherECommerce.Modules.Customers.Core/YetAnotherECommerce.Modules.Customers.Core.csproj \
--context CustomersDbContext \
--output migrations/Customers/migrate

dotnet ef migrations bundle --no-build --force \
--project ./apps/backend/src/Modules/Catalog/YetAnotherECommerce.Modules.Catalog.Core/YetAnotherECommerce.Modules.Catalog.Core.csproj \
--context CatalogDbContext \
--output migrations/Catalog/migrate

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
migrations/Customers/migrate --connection $DB_CONNECTION_STRING
migrations/Catalog/migrate --connection $DB_CONNECTION_STRING
migrations/Orders/migrate --connection $DB_CONNECTION_STRING
migrations/Carts/migrate --connection $DB_CONNECTION_STRING

echo 'Cleaning up...'
rm -r migrations