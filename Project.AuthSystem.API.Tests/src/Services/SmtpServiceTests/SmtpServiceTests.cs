
using FluentAssertions;
using Moq;
using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.SmtpService;

namespace Project.AuthSystem.API.Tests.src.Services.SmtpServiceTests;
public class SmtpServiceTests
{
    private readonly Mock<IAppSettings> mockAppSettings = new Mock<IAppSettings>();

    [Fact]
    public void SmtpService_SendEmail_ReturnThrowApiException()
    {
        // Arrange

        var emailInfos = MockEmail();

        var smtpService = new SmtpService(mockAppSettings.Object);

        mockAppSettings.Setup(x => x.SmtpUsername).Returns("John Doe");
        mockAppSettings.Setup(x => x.SmtpPassword).Returns("tes tad os1");
        mockAppSettings.Setup(x => x.SmtpEmail).Returns("johndoe@provider.com");

        // Act

        var result = () => smtpService.SendEmail(emailInfos);

        // Assert

        result.Should().Throw<ApiException>().WithMessage("Erro ao enviar email, tente novamente mais tarde");
    }


    private static Email MockEmail() => new Email("Jane Doe", "janedoe@provider.com", "Email de teste", "Esse é um email de teste");
}