using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands;

public class ChangePasswordCommandHandlerTests
{
    private readonly Mock<FakeUserManager>  _userManagerMock = new();
    private readonly ChangePasswordCommandHandler _sut;
    
    public ChangePasswordCommandHandlerTests()
    {
        _sut = new ChangePasswordCommandHandler(_userManagerMock.Object);
    }

    [Theory, AutoData]
    public async Task WhenUserDoesNotExist_ThenShouldThrowException(ChangePasswordCommand command)
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);
        var expectedException = new UserNotExistException(command.UserId);
        
        // Act
        var result = await Assert.ThrowsAsync<UserNotExistException>(() => _sut.HandleAsync(command));

        // Assert
        result.ShouldNotBeNull();
        result.ErrorCode.ShouldBe(expectedException.ErrorCode);
        result.Message.ShouldBe(expectedException.Message);
    }
}