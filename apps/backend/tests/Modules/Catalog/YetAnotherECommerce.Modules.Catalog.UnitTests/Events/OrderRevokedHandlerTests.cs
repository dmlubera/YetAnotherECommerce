using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Catalog.Core.Entitites;
using YetAnotherECommerce.Modules.Catalog.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Catalog.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Catalog.Core.Repositories;
using YetAnotherECommerce.Modules.Catalog.UnitTests.Fixtures.Entities;

namespace YetAnotherECommerce.Modules.Catalog.UnitTests.Events;

public class OrderRevokedHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly OrderRevokedHandler _handler;

    public OrderRevokedHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new OrderRevokedHandler(_productRepositoryMock.Object);
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
        var productsFromRevokedOrder = new Dictionary<Guid, int>();
        products.ForEach(x => productsFromRevokedOrder.Add(x.Id, 1));

        var orderCreated = new OrderRevoked(Guid.NewGuid(), productsFromRevokedOrder);
        _productRepositoryMock
            .Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(products);

        await _handler.HandleAsync(orderCreated);

        products[0].Quantity.Value
            .ShouldBe(quanitiesBeforeUpdate.GetValueOrDefault(products[0].Id) + productsFromRevokedOrder.GetValueOrDefault(products[0].Id));
        products[1].Quantity.Value
            .ShouldBe(quanitiesBeforeUpdate.GetValueOrDefault(products[1].Id) + productsFromRevokedOrder.GetValueOrDefault(products[1].Id));
        _productRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<IEnumerable<Product>>()));
    }
}