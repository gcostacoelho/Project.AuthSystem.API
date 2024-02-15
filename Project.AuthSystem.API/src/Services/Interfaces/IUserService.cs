using Project.AuthSystem.API.src.Models.Users;

namespace Project.AuthSystem.API.src.Services.Interfaces;
public interface IUserService
{
    Task<User> GetUserAsync(string email);
    Task<User> NewUserAsync(UserDto user);
    Task<User> UpdateUserAsync(UserDtoWithoutPass user, string email);
    Task DeleteUserAsync (string email);
    Task<string> UpdatePassword(string email, string newPassword, string oldPassword);
}