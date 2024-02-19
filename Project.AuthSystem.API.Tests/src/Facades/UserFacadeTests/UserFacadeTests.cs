using FluentAssertions;
using Moq;
using Project.AuthSystem.API.src.Facades.UserFacade;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.Tests.src.Facades.UserFacadeTests;
public class UserFacadeTests
{
    private readonly Mock<ISmtpService> mockSmtpService = new Mock<ISmtpService>();
    private readonly Mock<IUserService> mockUserService = new Mock<IUserService>();

    private readonly UserFacade _userFacade;

    public UserFacadeTests()
    {
        _userFacade = new UserFacade(mockSmtpService.Object, mockUserService.Object);
    }

    [Fact]
    public async void UserFacade_GetUserAsync_ReturnApiResponseUser()
    {
        // Arrange
        var user = MockUser();

        mockUserService.Setup(x => x.GetUserAsync("janedoe@provider.com")).ReturnsAsync(user);

        // Act

        var result = await _userFacade.GetUserAsync("janedoe@provider.com");

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<ApiResponse<User>>();
    }

    [Fact]
    public async void UserFacade_NewUserAsync_ReturnApiResponseUser()
    {
        // Arrange
        var user = MockUserDto();

        // Act

        var result = await _userFacade.NewUserAsync(user);

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<ApiResponse<User>>();
    }

    [Fact]
    public async void UserFacade_UpdatePassword_ReturnString()
    {
        // Arrange
        var user = MockUser();

        mockUserService.Setup(x => x.GetUserAsync(user.Email)).ReturnsAsync(user);
        mockUserService.Setup(x => x.UpdatePassword(user.Email, "test12345", user.Password)).ReturnsAsync("Senha alterada com sucesso");

        // Act

        var result = await _userFacade.UpdatePassword(user.Email, "test12345", user.Password);

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<string>();
    }

    [Fact]
    public async void UserFacade_UpdateUserAsync_ReturnApiResponseUser()
    {
        // Arrange
        var user = MockUser();
        var userUpdateRequest = MockUserDtoWithoutPass();

        mockUserService.Setup(x => x.GetUserAsync(user.Email)).ReturnsAsync(user);

        // Act

        var result = await _userFacade.UpdateUserAsync(userUpdateRequest, user.Email);

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<ApiResponse<User>>();
    }

    [Fact]
    public async void UserFacade_DeleteUserAsync_ReturnVoid()
    {
        // Arrange
        var user = MockUser();

        // Act

        await _userFacade.DeleteUserAsync(user.Email);

        // Assert
        Assert.True(true);
    }

    private static User MockUser() => new User("janedoe@provider.com", "test1234", "Jane Doe", DateTime.UtcNow, Guid.NewGuid());
    private static UserDtoWithoutPass MockUserDtoWithoutPass() => new UserDtoWithoutPass { Email = "janedoe1985@provider.com", Birthday = DateTime.UtcNow, Fullname = "Jane Doe" };
    private static UserDto MockUserDto() => new UserDto { Email = "janedoe@provider.com", Birthday = DateTime.UtcNow, Fullname = "Jane Doe", Password = "test1234" };
}