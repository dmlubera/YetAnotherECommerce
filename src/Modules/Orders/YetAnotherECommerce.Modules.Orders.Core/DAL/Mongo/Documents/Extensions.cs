using System.Collections.Generic;
using System.Linq;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Documents
{
    public static class Extensions
    {
        public static OrderDocument AsDocument(this Order entity)
            => new OrderDocument(entity.Id, entity.CustomerId, entity.Status, entity.OrderItems.AsDocument(), entity.CreatedAt, entity.LastUpdatedAt);

        public static List<OrderItemDocument> AsDocument(this IReadOnlyCollection<OrderItem> entities)
            => entities.Select(x => new OrderItemDocument(x.Id, x.ProductId, x.Name, x.UnitPrice, x.Quantity)).ToList();

        public static Order AsEntity(this OrderDocument document)
            => new Order(document.Id, document.CustomerId, document.Status, document.OrderItems.AsEntity(), document.CreatedAt, document.LastUpdatedAt);

        public static List<OrderItem> AsEntity(this IReadOnlyCollection<OrderItemDocument> documents)
            => documents.Select(x => new OrderItem(x.Id, x.ProductId, x.Name, x.UnitPrice, x.Quantity)).ToList();
    }
}