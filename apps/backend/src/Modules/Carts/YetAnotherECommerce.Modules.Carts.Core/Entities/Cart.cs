using System;
using System.Collections.Generic;
using System.Linq;

namespace YetAnotherECommerce.Modules.Carts.Core.Entities
{
    public class Cart
    {
        private readonly List<CartItem> _items;
        public decimal Total => _items.Sum(x => x.TotalPrice);
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        public Cart()
        {
            _items = new List<CartItem>();
        }

        public void AddItem(CartItem item)
        {
            var existedItem = _items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existedItem is not null)
            {
                if (existedItem.UnitPrice != item.UnitPrice)
                    existedItem.UpdatePrice(item.UnitPrice);

                existedItem.IncreaseQuantity(item.Quantity);
            }
            else
            {
                _items.Add(item);
            }
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(x => x.Id == itemId);
            _items.Remove(item);
        }
    }
}