using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Commands;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Commands;

public class CancelOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IMessageBroker> _messageBrokerMock;
    private readonly Mock<ILogger<CancelOrderComandHandler>> _loggerMock;
    private readonly CancelOrderComandHandler _handler;

    public CancelOrderCommandHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _messageBrokerMock = new Mock<IMessageBroker>();
        _loggerMock = new Mock<ILogger<CancelOrderComandHandler>>();
        _handler = new CancelOrderComandHandler(_orderRepositoryMock.Object, _messageBrokerMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task WhenOrderNotExistsOrIsNotCustomerOrder_ThenShouldThrowAnException()
    {
        var command = new CancelOrderCommand(Guid.NewGuid(), Guid.NewGuid());
        var expectedException = new NoSuchOrderExistsForCustomerException(command.OrderId, command.CustomerId);
        _orderRepositoryMock
            .Setup(x => x.GetForCustomerByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var exception = await Assert.ThrowsAsync<NoSuchOrderExistsForCustomerException>(() => _handler.HandleAsync(command));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()), Times.Never);
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderCanceled>()), Times.Never);
    }

    [Fact]
    public async Task WhenOrderExist_ThenShouldThrowAnException()
    {
        var command = new CancelOrderCommand(Guid.NewGuid(), Guid.NewGuid());
        var order = new Order(command.CustomerId, []);
        order.AcceptOrder();
        _orderRepositoryMock
            .Setup(x => x.GetForCustomerByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(order);

        await _handler.HandleAsync(command);

        order.Status.ShouldBe(OrderStatus.Canceled);
        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()));
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderCanceled>()));
    }
}