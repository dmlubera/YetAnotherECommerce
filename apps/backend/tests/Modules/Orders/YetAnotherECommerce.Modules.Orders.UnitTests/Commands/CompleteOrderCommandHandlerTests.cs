using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Commands;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Commands;

public class CompleteOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
    private readonly CompleteOrderCommandHandler _handler;

    public CompleteOrderCommandHandlerTests()
    {
        _handler = new CompleteOrderCommandHandler(_orderRepositoryMock.Object,
            NullLogger<CompleteOrderCommandHandler>.Instance);
    }

    [Fact]
    public async Task WhenOrderNotExist_ThenShouldThrowAnException()
    {
        var command = new CompleteOrderCommand(Guid.NewGuid());
        var expectedException = new OrderDoesNotExistException(command.OrderId);
        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var exception = await Assert.ThrowsAsync<OrderDoesNotExistException>(() => _handler.HandleAsync(command));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task WhenOrderExist_ThenShouldUpdateOrderStatus()
    {
        var command = new CompleteOrderCommand(Guid.NewGuid());
        var order = new Order(Guid.NewGuid(), []);
        order.AcceptOrder();
        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(order);

        await _handler.HandleAsync(command);

        order.Status.ShouldBe(OrderStatus.Completed);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()));
    }
}