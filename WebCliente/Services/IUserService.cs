using WebCliente.Models;

namespace WebCliente.Services
{
    public interface IUserService
    {
        Task<List<UserDto>?> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(string userId);
        Task<bool> CreateUserAsync(CreateUserRequest request);
        Task<bool> UpdateUserAsync(string userId, UpdateUserRequest request);
        Task<bool> DeleteUserAsync(string userId);
    }
}