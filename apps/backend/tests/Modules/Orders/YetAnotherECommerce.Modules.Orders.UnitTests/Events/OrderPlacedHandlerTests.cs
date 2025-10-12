using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Events
{
    public class OrderPlacedHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly OrderPlacedHandler _handler;

        public OrderPlacedHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _handler = new OrderPlacedHandler(_orderRepositoryMock.Object, _customerRepositoryMock.Object,
                _messageBrokerMock.Object);
        }

        [Fact]
        public async Task WhenCustomerNotExist_ThenShouldThrowAnException()
        {
            var @event = new OrderPlaced(Guid.NewGuid(), new List<ProductDto>());
            var expectedException = new CustomerWithGivenIdDoesNotExistsException(@event.CustomerId);
            _customerRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var exception = await Assert.ThrowsAsync<CustomerWithGivenIdDoesNotExistsException>(() => _handler.HandleAsync(@event));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
            _orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()), Times.Never);
            _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderPlaced>()), Times.Never);
        }

        [Fact]
        public async Task WhenCustomerExist_ThenShouldAddOrderAndPublishIntegrationEvent()
        {
            var @event = new OrderPlaced(Guid.NewGuid(), new List<ProductDto>()
            {
                new ProductDto(Guid.NewGuid(), "ProductName", 10, 1),
                new ProductDto(Guid.NewGuid(), "ProductName", 12, 4),
            });
            var customer = new Customer(Guid.NewGuid(), "John", "Doe", "johndoe@yetanotherecommerce.com", "Groove Street");
            _customerRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            await _handler.HandleAsync(@event);

            _orderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>()));
            _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderCreated>()));
        }
    }
}