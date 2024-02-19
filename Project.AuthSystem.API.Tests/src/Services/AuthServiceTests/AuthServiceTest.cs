using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Services.AuthService;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.Tests.src.Services.AuthServiceTests;
public class AuthServiceTest
{
    private readonly Mock<IAppSettings> mockAppSettings = new Mock<IAppSettings>();

    [Fact]
    public void AuthService_GenerateTokenAsync_ReturnString()
    {
        // Arrange

        var mockedUser = MockUser();

        var authService = new AuthService(mockAppSettings.Object);

        mockAppSettings.Setup(x => x.Secret).Returns("47Rq9da61QGHBPsRy60cwtIeRTP26bzvb7l49EReh9W/qSRGvmJ2WOzHzUMpO2Sm");

        // Act

        var result = authService.GenerateTokenAsync(mockedUser);

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<string>();
    }

    private static User MockUser() => new User("teste@teste.com", "root", "Teste Teste", DateTime.UtcNow, Guid.NewGuid());
}