using FluentAssertions;
using Moq;


using Project.AuthSystem.API.src.Models;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Facades.Login;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.Tests.src.Facades.LoginFacadeTests;
public class LoginFacadeTests
{
    private readonly Mock<IAuthService> mockAuthService = new Mock<IAuthService>();
    private readonly Mock<IUserService> mockUserService = new Mock<IUserService>();
    private readonly Mock<IHashService> mockHashService = new Mock<IHashService>();

    private readonly LoginFacade _loginFacade;

    public LoginFacadeTests()
    {
        _loginFacade = new LoginFacade(mockAuthService.Object, mockUserService.Object, mockHashService.Object);
    }

    [Fact]
    public async void LoginFacade_GetTokenAsync_ReturnString()
    {
        // Arrange

        var user = MockUser();

        mockUserService.Setup(x => x.GetUserAsync("janedoe@provider.com")).ReturnsAsync(user);
        mockHashService.Setup(x => x.CompareHashes("test1234", "test1234")).Returns(true);

        // Act

        var result = await _loginFacade.GetTokenAsync("janedoe@provider.com", "test1234");

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<string>();
    }


    [Fact]
    public async void LoginFacade_GetTokenAsync_ReturnThrowApiException()
    {
        // Arrange

        var user = MockUser();

        mockUserService.Setup(x => x.GetUserAsync("janedoe@provider.com")).ReturnsAsync(user);
        mockHashService.Setup(x => x.CompareHashes("test123", "test1234")).Returns(false);

        // Act

        Func<Task> result = async () => await _loginFacade.GetTokenAsync("janedoe@provider.com", "test1234");

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.UNAUTHORIZED);
    }

    private static User MockUser() => new User("janedoe@provider.com", "test1234", "Jane Doe", DateTime.UtcNow, Guid.NewGuid());
}