using System.Net.Http.Json;
using WebCliente.Models;

namespace WebCliente.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;

        public UserService(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<UserDto>?> GetAllUsersAsync()
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var response = await client.GetAsync("/api/users/list-all");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<UserDto>>();
                }

                Console.WriteLine($"Erro ao buscar usuários: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuários: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var response = await client.GetAsync($"/api/users/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }

                Console.WriteLine($"Erro ao buscar usuário: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var response = await client.PostAsJsonAsync("/api/users", request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                Console.WriteLine($"Erro ao criar usuário: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar usuário: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var response = await client.PutAsJsonAsync($"/api/users/{userId}", request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                Console.WriteLine($"Erro ao atualizar usuário: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var response = await client.DeleteAsync($"/api/users/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                Console.WriteLine($"Erro ao excluir usuário: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir usuário: {ex.Message}");
                return false;
            }
        }
    }
}