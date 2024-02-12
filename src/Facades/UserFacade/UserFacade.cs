using Project.AuthSystem.API.src.Facades.Interfaces.UserFacadeInterface;
using Project.AuthSystem.API.src.Services.Interfaces;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.src.Facades.UserFacade;
public class UserFacade(ISmtpService smtpService, IUserService userService) : IUserFacade
{
    private readonly ISmtpService _smtpService = smtpService;
    private readonly IUserService _userService = userService;

    private const string ACCOUNT_CREATED_SUBJECT = "{0}, conta criada com sucesso";

    public async Task<User> GetUserAsync(string email)
    {
        var user = await _userService.GetUserAsync(email);

        return user;
    }

    public async Task<User> NewUserAsync(UserDto user)
    {
        var userResponse = await _userService.NewUserAsync(user);

        try
        {
            var emailBody = string.Format("\n\nNome: {0}\nEmail: {1}\nData de Nascimento: {2}", userResponse.Fullname, userResponse.Email, userResponse.Birthday.ToString());

            var emailInfos = new Email(
                userResponse.Fullname,
                userResponse.Email,
                string.Format(ACCOUNT_CREATED_SUBJECT, userResponse.Fullname),
                emailBody
            );

            _smtpService.SendEmail(emailInfos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return userResponse;
    }

    public async Task<string> UpdatePassword(string email, string newPassword, string oldPassword)
    {
        var userInfos = await _userService.GetUserAsync(email);
        var updatePasswordResponse = await _userService.UpdatePassword(email, newPassword, oldPassword);

        try
        {
            var emailBody = "Sua senha foi alterada, caso não tenha sido você, por favor entre em contato com nosso suporte";

            var emailInfos = new Email(
                userInfos.Fullname,
                email,
                string.Format("{0}, senha atualizada com sucesso", userInfos.Fullname),
                emailBody
            );

            _smtpService.SendEmail(emailInfos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return updatePasswordResponse;

    }

    public async Task<User> UpdateUserAsync(UserDtoWithoutPass user, string email)
    {
        var userUpdatedResponse = await _userService.UpdateUserAsync(user, email);

        return userUpdatedResponse;
    }

    public async Task DeleteUserAsync(string email)
    {
        await _userService.DeleteUserAsync(email);
    }
}