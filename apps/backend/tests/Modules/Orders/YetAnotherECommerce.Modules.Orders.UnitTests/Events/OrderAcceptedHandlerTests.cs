using System;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Events;

public class OrderAcceptedHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly OrderAcceptedHandler _handler;

    public OrderAcceptedHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _handler = new OrderAcceptedHandler(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task WhenOrderNotExist_ThenShouldThrowAnException()
    {
        var @event = new OrderAccepted(Guid.NewGuid());
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
        var @event = new OrderAccepted(Guid.NewGuid());
        var order = new Order(Guid.NewGuid(), []);
        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(order);

        await _handler.HandleAsync(@event);

        order.Status.ShouldBe(OrderStatus.Accepted);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()));
    }
}