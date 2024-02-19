using FluentAssertions;
using Moq;
using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Services.HashService;

namespace Project.AuthSystem.API.Tests.src.Services.HashServicesTests;
public class HashServicesTest
{
    private readonly Mock<IAppSettings> mockAppSettings = new Mock<IAppSettings>();

    [Fact]
    public void HashService_EncryptyText_ReturnString()
    {
        // Arrange
        var hashService = new HashService(mockAppSettings.Object);

        mockAppSettings.Setup(x => x.HashSalt).Returns("tests");

        // Act

        var result = hashService.EncryptyText("johnDoe");

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<string>();
    }

    [Theory]
    [InlineData("test", true)]
    [InlineData("teste", false)]
    public void HashService_CompareHashes_ReturnFalseOrTrue(string entry, bool expectedResult)
    {
        // Arrange

        var hashService = new HashService(mockAppSettings.Object);
        
        mockAppSettings.Setup(x => x.HashSalt).Returns("tests");

        // Act

        var result = hashService.CompareHashes("111971BDDC35C725F498740BB35B4B20E0116128DC7EF03784132F62AB03DE50", entry);

        //Assert

        result.Should().Be(expectedResult);
    }
}