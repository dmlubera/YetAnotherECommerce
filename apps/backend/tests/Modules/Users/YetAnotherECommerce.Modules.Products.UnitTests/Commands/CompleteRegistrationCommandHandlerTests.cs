using System;
using System.Threading.Tasks;
using Bogus;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Users.Core.Commands;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.Events;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands;

public class CompleteRegistrationCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IMessagePublisher> _messageBrokerMock = new();
    private readonly CompleteRegistrationCommandHandler _handler;

    public CompleteRegistrationCommandHandlerTests()
    {
        _handler = new CompleteRegistrationCommandHandler(_userRepositoryMock.Object, _messageBrokerMock.Object);
    }

    [Fact]
    public async Task WhenRegistrationAlreadyCompleted_ThenShouldThrowAnException()
    {
        var user = CreateUserFixture();
        user.CompleteRegistration();
        var command = CreateCommand();
        var expectedException = new RegistrationAlreadyCompletedException();
        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(user);

        var exception = await Assert.ThrowsAsync<RegistrationAlreadyCompletedException>(() => _handler.HandleAsync(command));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public async Task WhenDataIsValid_ThenShouldUpdateUserAndPublishIntegrationEvent()
    {
        var user = CreateUserFixture();
        var command = CreateCommand();
        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(user);

        await _handler.HandleAsync(command);

        user.FirstName.Value.ShouldBe(command.FirstName);
        user.LastName.Value.ShouldBe(command.LastName);
        user.Address.Street.ShouldBe(command.Street);
        user.Address.City.ShouldBe(command.City);
        user.Address.ZipCode.ShouldBe(command.ZipCode);
        user.Address.Country.ShouldBe(command.Country);
        user.IsRegistrationCompleted.ShouldBeTrue();
        _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()));
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<RegistrationCompleted>()));
    }

    private static CompleteRegistrationCommand CreateCommand()
        => new(
            userId: Guid.NewGuid(),
            firstName: "Carl",
            lastName: "Johnson",
            street: "Grove Street",
            city: "Los Santos",
            zipCode: "555-1111",
            country: "USA");

    private static User CreateUserFixture()
        => new Faker<User>()
            .CustomInstantiator(x => Activator.CreateInstance(typeof(User), nonPublic: true) as User)
            .RuleFor(x => x.Id, f => new AggregateId())
            .Generate();
}