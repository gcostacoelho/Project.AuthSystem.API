using Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using Project.AuthSystem.API.src.Database;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Services.Interfaces;
using Project.AuthSystem.API.src.Services.UserService;

namespace Project.AuthSystem.API.Tests.src.Services.UserServicesTests;
public class UserServiceTest
{
    private readonly Mock<IHashService> _hashService = new Mock<IHashService>();

    [Fact]
    public async void UserService_GetUserAsync_ReturnUser()
    {
        #region Arrange

        var mockedUser = MockUser();
        var dbContext = await GetAppDbContextAsync();

        var userService = new UserService(dbContext, _hashService.Object);

        #endregion

        #region Act

        var result = await userService.GetUserAsync(mockedUser.Email);

        #endregion

        #region Assert 

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();

        #endregion
    }

    [Fact]
    public async void UserService_NewUserAsync_ReturnUser()
    {
        #region Arrange

        var dbContext = await GetAppDbContextAsync();

        var userService = new UserService(dbContext, _hashService.Object);


        var user = new UserDto { Email = "teste@gmail.com", Password = "rooot", Fullname = "Teste Teste Teste", Birthday = DateTime.UtcNow };

        _hashService.Setup(x => x.EncryptyText(user.Password)).Returns("stringgg");

        #endregion

        #region Act

        var result = await userService.NewUserAsync(user);

        #endregion

        #region Assert 

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();

        #endregion
    }

    [Fact]
    public async void UserService_UpdateUserAsync_ReturnUser()
    {
        #region Arrange

        var mockedUser = MockUser();

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        var userUpdateBody = new UserDtoWithoutPass { Email = "test@gmail.com", Fullname = "John Doe", Birthday = DateTime.UtcNow };

        #endregion

        #region Act

        var result = await userService.UpdateUserAsync(userUpdateBody, mockedUser.Email);

        #endregion

        #region Assert

        result.Should().NotBeNull();

        result.Email.Should().Be(userUpdateBody.Email);
        result.Fullname.Should().Be(userUpdateBody.Fullname);
        result.Birthday.Should().Be(userUpdateBody.Birthday);

        #endregion
    }

    [Fact]
    public async void UserService_UpdatePasswordAsync_ReturnString()
    {
        #region Arrange

        var newPass = "JaneDoe1234";
        var mockedUser = MockUser();

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        _hashService.Setup(x => x.CompareHashes(mockedUser.Password, mockedUser.Password)).Returns(true);
        _hashService.Setup(x => x.EncryptyText(newPass)).Returns("stringgg");

        #endregion

        #region Act

        var result = await userService.UpdatePassword(mockedUser.Email, newPass, mockedUser.Password);

        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<string>();
        #endregion
    }

    [Fact]
    public async void UserService_DeleteUser_ReturnVoid()
    {
        #region Arrange

        var mockedUser = MockUser();

        var dbContext = await GetAppDbContextAsync();
        var userService = new UserService(dbContext, _hashService.Object);

        #endregion

        #region Act

        await userService.DeleteUserAsync(mockedUser.Email);

        #endregion

        #region Assert

        // ! How to test this? Because this method is a Task without return

        Assert.True(true);

        #endregion
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