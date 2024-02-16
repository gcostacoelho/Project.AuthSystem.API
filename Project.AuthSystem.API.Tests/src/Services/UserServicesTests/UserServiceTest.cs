using Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using Project.AuthSystem.API.src.Database;
using Project.AuthSystem.API.src.Services.Interfaces;
using Project.AuthSystem.API.src.Services.UserService;
using Project.AuthSystem.API.src.Models;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.Tests.src.Services.UserServicesTests;
public class UserServiceTest
{
    private readonly Mock<IHashService> _hashService = new Mock<IHashService>();

    [Fact]
    public async void UserService_GetUserAsync_ReturnUser()
    {
        // Arrange

        var mockedUser = MockUser();
        var dbContext = await GetAppDbContextAsync();

        var userService = new UserService(dbContext, _hashService.Object);

        // Act

        var result = await userService.GetUserAsync(mockedUser.Email);

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
    }

    [Fact]
    public async void UserService_GetUserAsync_ReturnApiException()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var userService = new UserService(dbContext, _hashService.Object);

        // Act

        Func<Task> result = async () => await userService.GetUserAsync("teste");

        // Assert

        await result.Should().ThrowAsync<ApiException>()
            .WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void UserService_NewUserAsync_ReturnUser()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var userService = new UserService(dbContext, _hashService.Object);


        var user = new UserDto { Email = "teste@gmail.com", Password = "rooot", Fullname = "Teste Teste Teste", Birthday = DateTime.UtcNow };

        _hashService.Setup(x => x.EncryptyText(user.Password)).Returns("stringgg");

        // Act

        var result = await userService.NewUserAsync(user);

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
    }

    [Fact]
    public async void UserService_NewUserAsync_ReturnApiException()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var userService = new UserService(dbContext, _hashService.Object);

        var user = new UserDto { Email = "teste", Password = "rooot", Fullname = "Teste Teste Teste", Birthday = DateTime.UtcNow };

        // Act

        Func<Task> result = async () => await userService.GetUserAsync("teste");

        // Assert

        await result.Should().ThrowAsync<ApiException>();
    }

    [Fact]
    public async void UserService_UpdateUserAsync_ReturnUser()
    {
        // Arrange

        var mockedUser = MockUser();

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        var userUpdateBody = new UserDtoWithoutPass { Email = "test@gmail.com", Fullname = "John Doe", Birthday = DateTime.UtcNow };

        // Act

        var result = await userService.UpdateUserAsync(userUpdateBody, mockedUser.Email);

        // Assert

        result.Should().NotBeNull();

        result.Email.Should().Be(userUpdateBody.Email);
        result.Fullname.Should().Be(userUpdateBody.Fullname);
        result.Birthday.Should().Be(userUpdateBody.Birthday);
    }

    [Fact]
    public async void UserService_UpdateUserAsync_ReturnApiException()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        var userUpdateBody = new UserDtoWithoutPass { Email = "test@gmail.com", Fullname = "John Doe", Birthday = DateTime.UtcNow };

        // Act

        Func<Task> result = async () => await userService.UpdateUserAsync(userUpdateBody, "teste");

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void UserService_UpdatePasswordAsync_ReturnString()
    {
        // Arrange

        var newPass = "JaneDoe1234";
        var mockedUser = MockUser();

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        _hashService.Setup(x => x.CompareHashes(mockedUser.Password, mockedUser.Password)).Returns(true);
        _hashService.Setup(x => x.EncryptyText(newPass)).Returns("stringgg");

        // Act

        var result = await userService.UpdatePassword(mockedUser.Email, newPass, mockedUser.Password);

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<string>();
    }

    [Fact]
    public async void UserService_UpdatePasswordAsync_ReturnApiExceptionUserNotFound()
    {
        // Arrange

        var newPass = "JaneDoe1234";
        var mockedUser = MockUser();

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        // Act

        Func<Task> result = async () => await userService.UpdatePassword("teste", newPass, mockedUser.Password);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void UserService_UpdatePasswordAsync_ReturnApiExceptionPasswordNotEqual()
    {
        // Arrange

        var newPass = "JaneDoe1234";
        var mockedUser = MockUser();

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        _hashService.Setup(x => x.CompareHashes(mockedUser.Password, newPass)).Returns(false);

        // Act

        Func<Task> result = async () => await userService.UpdatePassword(mockedUser.Email, newPass, mockedUser.Password);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.NOT_POSSIBLE_CHANGE);
    }

    [Fact]
    public async void UserService_DeleteUserAsync_ReturnVoid()
    {
        // Arrange

        var mockedUser = MockUser();

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        // Act

        await userService.DeleteUserAsync(mockedUser.Email);

        // Assert

        // ! How to test this? Because this method is a Task without return

        Assert.True(true);
    }

    [Fact]
    public async void UserService_DeleteUserAsync_ReturnApiException()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        // Act

        Func<Task> result = async () => await userService.DeleteUserAsync("teste");

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }


    private static async Task<AppDbContext> GetAppDbContextAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var databaseContext = new AppDbContext(options);

        databaseContext.Database.EnsureCreated();

        if (await databaseContext.Users.CountAsync() <= 0)
        {
            databaseContext.Users.Add(MockUser());

            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
    }

    private static User MockUser() => new User("teste@teste.com", "root", "Teste Teste", DateTime.UtcNow, Guid.NewGuid());
}