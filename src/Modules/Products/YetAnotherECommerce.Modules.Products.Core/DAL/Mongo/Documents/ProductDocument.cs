using System;
using YetAnotherECommerce.Shared.Abstractions.Mongo;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents
{
    public class ProductDocument : IDocument
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}