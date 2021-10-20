using System.Collections.Generic;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Documents
{
    public static class Extensions
    {
        public static OrderDocument AsDocument(this Order order)
        {
            return new OrderDocument
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                OrderItems = order.OrderItems.AsDocument()
            };
        }

        public static List<OrderItemDocument> AsDocument(this IReadOnlyCollection<OrderItem> orderItems)
        {
            var documents = new List<OrderItemDocument>();
            foreach (var item in orderItems)
            {
                documents.Add(new OrderItemDocument
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }
            return documents;
        }
    }
}