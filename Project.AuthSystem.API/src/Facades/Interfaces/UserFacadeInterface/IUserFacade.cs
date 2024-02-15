using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.src.Facades.Interfaces.UserFacadeInterface;
public interface IUserFacade
{
    Task<ApiResponse<User>> GetUserAsync(string email);
    Task<ApiResponse<User>> NewUserAsync(UserDto user);
    Task<ApiResponse<User>> UpdateUserAsync(UserDtoWithoutPass user, string email);
    Task<string> UpdatePassword(string email, string newPassword, string oldPassword);
    Task DeleteUserAsync(string email);
}