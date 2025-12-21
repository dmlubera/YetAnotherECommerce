using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Users.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Events;

public class UserRegisteredHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserRegisteredHandler _handler;

    public UserRegisteredHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new UserRegisteredHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task WhenUserRegistered_ThenShouldAddUserToUsersDatabase()
    {
        var @event = new UserRegistered(Guid.NewGuid(), "user@yetanotherecommerce.com");

        await _handler.HandleAsync(@event);

        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()));
    }
}