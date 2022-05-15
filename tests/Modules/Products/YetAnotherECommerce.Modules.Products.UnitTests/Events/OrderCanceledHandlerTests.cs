using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Entities;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Events
{
    public class OrderCanceledHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly OrderCanceledHandler _handler;

        public OrderCanceledHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new OrderCanceledHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task WhenValidData_ThenShouldUpdateProductsAndSaveChangesToDatabase()
        {
            var products = new List<Product>
            {
                ProductFixture.Create(),
                ProductFixture.Create()
            };
            products[0].UpdateQuantity(15);
            products[1].UpdateQuantity(17);
            
            var quanitiesBeforeUpdate = new Dictionary<Guid, int>();
            products.ForEach(x => quanitiesBeforeUpdate.Add(x.Id, x.Quantity));
            var productsFromCanceledOrder = new Dictionary<Guid, int>();
            products.ForEach(x => productsFromCanceledOrder.Add(x.Id, 1));

            var orderCreated = new OrderCanceled(Guid.NewGuid(), productsFromCanceledOrder);
            _productRepositoryMock
                .Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .ReturnsAsync(products);

            await _handler.HandleAsync(orderCreated);

            products[0].Quantity.Value
                .ShouldBe(quanitiesBeforeUpdate.GetValueOrDefault(products[0].Id) + productsFromCanceledOrder.GetValueOrDefault(products[0].Id));
            products[1].Quantity.Value
                .ShouldBe(quanitiesBeforeUpdate.GetValueOrDefault(products[1].Id) + productsFromCanceledOrder.GetValueOrDefault(products[1].Id));
            _productRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<IEnumerable<Product>>()));
        }
    }
}