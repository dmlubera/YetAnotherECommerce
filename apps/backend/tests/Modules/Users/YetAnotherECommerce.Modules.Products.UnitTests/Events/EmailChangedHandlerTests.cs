using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Users.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Events
{
    public class EmailChangedHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly EmailChangedHandler _handler;

        public EmailChangedHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new EmailChangedHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task WhenEmailChanged_ThenShouldUpdateEmailAndSaveChangesToDatabase()
        {
            var @event = new EmailChanged(Guid.NewGuid(), "newemail@yetanotherecommerce.com");
            var user = new User(Guid.NewGuid(), "email@yetanotherecommerce.com");
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            await _handler.HandleAsync(@event);

            user.Email.ShouldBe(@event.Email);
            _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsIn(user)), Times.Once);
        }
    }
}