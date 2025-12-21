using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;


namespace YetAnotherECommerce.Modules.Orders.UnitTests.Events;

public class OrderRejectedHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly OrderRejectedHandler _handler;

    public OrderRejectedHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _handler = new OrderRejectedHandler(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task WhenOrderNotExist_ThenShouldThrowAnException()
    {
        var @event = new OrderRejected(Guid.NewGuid());
        var expectedException = new OrderDoesNotExistException(@event.OrderId);
        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var exception = await Assert.ThrowsAsync<OrderDoesNotExistException>(() => _handler.HandleAsync(@event));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task WhenOrderExist_ThenShouldUpdateStatus()
    {
        var @event = new OrderRejected(Guid.NewGuid());
        var order = new Order(Guid.NewGuid(), new List<OrderItem>());
        order.AcceptOrder();
        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(order);

        await _handler.HandleAsync(@event);

        order.Status.ShouldBe(OrderStatus.Rejected);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()));
    }
}