using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Messages.Events;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Carts.Core.Events.External.Handlers
{
    public class ProductAddedToCartHandler : IEventHandler<ProductAddedToCart>
    {
        public Task HandleAsync(ProductAddedToCart @event)
        {
            throw new System.NotImplementedException();
        }
    }
}