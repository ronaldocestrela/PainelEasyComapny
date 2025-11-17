using System.Net.Http;
using System.Net.Http;
using WebCliente.Models;

namespace WebCliente.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task LogoutAsync();
        Task InitializeAsync();
        Task SaveTokenAfterRenderAsync();
        bool IsAuthenticated { get; }
        string? Token { get; }
        HttpClient CreateAuthenticatedClient();
        Task<bool> IsUserAdminAsync();
    }
}