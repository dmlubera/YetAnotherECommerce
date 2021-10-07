using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents
{
    public static class Extensions
    {
        public static ProductDocument AsDocument(this Product product)
            => new ProductDocument
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity
            };

        public static Product AsEntity(this ProductDocument document)
            => new Product(document.Id,
                document.Name,
                document.Description,
                document.Price,
                document.Quantity);
    }
}