using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Queries;

public class GetOrderDetailsQueryHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly GetOrderDetailsQueryHandler _handler;

    public GetOrderDetailsQueryHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _handler = new GetOrderDetailsQueryHandler(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task WhenOrderWithGivenIdDoesNotExist_ThenShuldThrowAnException()
    {
        var query = new GetOrderDetailsQuery(Guid.NewGuid(), Guid.NewGuid());
        var expectedException = new OrderDoesNotExistException(query.OrderId);
        _orderRepositoryMock
            .Setup(x => x.GetForCustomerByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var exception = await Assert.ThrowsAsync<OrderDoesNotExistException>(() => _handler.HandleAsync(query));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public async Task WhenOrderWithGivenIdExist_ThenShouldMapToDto()
    {
        var query = new GetOrderDetailsQuery(Guid.NewGuid(), Guid.NewGuid());
        var order = new Order(query.CustomerId, new List<OrderItem>());
        _orderRepositoryMock
            .Setup(x => x.GetForCustomerByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(order);

        var result = await _handler.HandleAsync(query);

        result.ShouldNotBeNull();
        result.Status.ShouldBe(order.Status.ToString());
        result.OrderItems.Count.ShouldBe(order.OrderItems.Count);
        result.TotalPrice.ShouldBe(order.OrderItems.Sum(x => x.UnitPrice * x.Quantity));
    }
}