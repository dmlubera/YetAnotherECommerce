using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Events
{
    public class RegistrationCompletedHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly RegistrationCompletedHandler _handler;

        public RegistrationCompletedHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _handler = new RegistrationCompletedHandler(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task WhenEventComes_ThenShouldAddCustomerToDatabase()
        {
            var @event = new RegistrationCompleted(Guid.NewGuid(), "John", "Doe", "johndoe@yetanotherecommerce.com", "Groove Street");
            
            await _handler.HandleAsync(@event);
            
            _customerRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Customer>()));
        }
    }
}