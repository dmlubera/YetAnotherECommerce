using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Commands;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Commands;

public class RevokeOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IMessageBroker> _messageBrokerMock;
    private readonly Mock<ILogger<RevokeOrderCommandHandler>> _loggerMock;
    private readonly RevokeOrderCommandHandler _handler;

    public RevokeOrderCommandHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _messageBrokerMock = new Mock<IMessageBroker>();
        _loggerMock = new Mock<ILogger<RevokeOrderCommandHandler>>();
        _handler = new RevokeOrderCommandHandler(_orderRepositoryMock.Object, _messageBrokerMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task WhenOrderNotExist_ThenShouldThrowAnException()
    {
        var command = new RevokeOrderCommand(Guid.NewGuid());
        var expectedException = new OrderDoesNotExistException(command.OrderId);
        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var exception = await Assert.ThrowsAsync<OrderDoesNotExistException>(() => _handler.HandleAsync(command));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()), Times.Never);
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderRevoked>()), Times.Never);
    }

    [Fact]
    public async Task WhenOrderExist_ThenShouldUpdateOrderStatus()
    {
        var command = new RevokeOrderCommand(Guid.NewGuid());
        var order = new Order(Guid.NewGuid(), new List<OrderItem>());
        order.AcceptOrder();
        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(order);

        await _handler.HandleAsync(command);

        order.Status.ShouldBe(OrderStatus.Rejected);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()));
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderRevoked>()));
    }
}