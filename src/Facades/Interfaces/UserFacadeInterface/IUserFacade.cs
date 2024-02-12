using Project.AuthSystem.API.src.Models.Users;

namespace Project.AuthSystem.API.src.Facades.Interfaces.UserFacadeInterface;
public interface IUserFacade
{
    Task<User> GetUserAsync(string email);
    Task<User> NewUserAsync(UserDto user);
    Task<User> UpdateUserAsync(UserDtoWithoutPass user, string email);
    Task<string> UpdatePassword(string email, string newPassword, string oldPassword);
    Task DeleteUserAsync(string email);
}