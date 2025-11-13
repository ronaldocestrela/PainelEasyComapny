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
    }
}