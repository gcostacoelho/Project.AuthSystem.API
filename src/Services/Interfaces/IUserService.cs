using Project.AuthSystem.API.src.Models.Users;

namespace Project.AuthSystem.API.src.Services.Interfaces;
public interface IUserService
{
    Task<User> GetUserAsync(Guid identity);
    Task<User> NewUserAsync(UserDto user);
    Task<User> UpdateUserAsync(UserDtoWithoutPass user, Guid identity);
    Task DeleteUserAsync (Guid identity);
    Task<string> UpdatePassword(Guid identity, string newPassword, string oldPassword);
}