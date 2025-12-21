using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Events;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Entities;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Events;

public class OrderCreatedHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMessageBroker> _messageBrokerMock;
    private readonly Mock<ILogger<OrderCreatedHandler>> _loggerMock;
    private readonly OrderCreatedHandler _handler;

    public OrderCreatedHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _messageBrokerMock = new Mock<IMessageBroker>();
        _loggerMock = new Mock<ILogger<OrderCreatedHandler>>();
        _handler = new OrderCreatedHandler(_productRepositoryMock.Object, _messageBrokerMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task WhenSomeOfOrderedProductsAreNotAvailable_ThenShouldThrowAnExceptionAndPublishIntegrationEvent()
    {
        var orderedProducts = new Dictionary<Guid, int>
        {
            { Guid.NewGuid(), 1 },
            { Guid.NewGuid(), 1 }
        };
        var orderCreated = new OrderCreated(Guid.NewGuid(), orderedProducts);
        var products = new List<Product>
        {
            ProductFixture.Create()
        };
        var expectedException = new SomeOfOrderedProductsAreNotAvailableException();
        _productRepositoryMock
            .Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(products);

        var exception = await Assert.ThrowsAsync<SomeOfOrderedProductsAreNotAvailableException>(() => _handler.HandleAsync(orderCreated));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderRejected>()));
    }

    [Fact]
    public async Task WhenSomeOfOrderedProcutsIsNotAvailableInOrderedQuantity_ThenShouldThrowAnExceptionAndPublishIntegrationEvent()
    {
        var products = new List<Product>
        {
            ProductFixture.Create()
        };
        products[0].UpdateQuantity(5);
        var orderedProducts = new Dictionary<Guid, int> { { products[0].Id, 10 } };
        var orderCreated = new OrderCreated(Guid.NewGuid(), orderedProducts);
        var expectedException = new ProductIsNotAvailableInOrderedQuantityException();
        _productRepositoryMock
            .Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(products);

        var exception = await Assert.ThrowsAsync<ProductIsNotAvailableInOrderedQuantityException>(() => _handler.HandleAsync(orderCreated));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderRejected>()));
    }

    [Fact]
    public async Task WhenOrderedProductsAreAvailable_ThenShouldUpdateQuanitityAndPublishIntegrationEvent()
    {
        var products = new List<Product>
        {
            ProductFixture.Create()
        };
        var originalQuantity = products[0].Quantity.Value;
        var orderedProducts = new Dictionary<Guid, int> { { products[0].Id, 1 } };
        var orderCreated = new OrderCreated(Guid.NewGuid(), orderedProducts);
        _productRepositoryMock
            .Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(products);

        await _handler.HandleAsync(orderCreated);


        products[0].Quantity.Value.ShouldBe(originalQuantity - orderedProducts.GetValueOrDefault(products[0].Id));
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderAccepted>()));
    }
}