using System.Net.Http.Json;
using Microsoft.JSInterop;
using WebCliente.Models;

namespace WebCliente.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IJSRuntime _jsRuntime;
        private string? _token;
        private const string API_BASE_URL = "http://localhost:5150";
        private const string TOKEN_KEY = "auth_token";

        public AuthService(IHttpClientFactory httpClientFactory, IJSRuntime jsRuntime)
        {
            _httpClientFactory = httpClientFactory;
            _jsRuntime = jsRuntime;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_token);
        public string? Token => _token;

        public HttpClient CreateAuthenticatedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            if (!string.IsNullOrEmpty(_token))
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            }
            return client;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var response = await client.PostAsJsonAsync($"{API_BASE_URL}/api/login", request);
                
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    
                    if (loginResponse != null)
                    {
                        _token = loginResponse.AccessToken;
                        
                        // Não tentar salvar no localStorage durante o login
                        // Isso será feito depois em OnAfterRenderAsync
                    }
                    
                    return loginResponse;
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer login: {ex.Message}");
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            _token = null;
            
            // Tentar remover o token do localStorage apenas se o JavaScript estiver disponível
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
            }
            catch (InvalidOperationException)
            {
                // JavaScript não está disponível durante renderização estática
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Sempre tentar recuperar o token do localStorage (permite reinicializações)
                _token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
                
                if (!string.IsNullOrEmpty(_token))
                {
                    // Token recuperado, será usado quando CreateAuthenticatedClient() for chamado
                    Console.WriteLine($"Token recuperado do localStorage com sucesso. Token length: {_token.Length}");
                }
                else
                {
                    Console.WriteLine("Nenhum token encontrado no localStorage");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inicializar AuthService: {ex.Message}");
            }
        }

        public async Task<bool> IsUserAdminAsync()
        {
            if (!IsAuthenticated || string.IsNullOrEmpty(_token))
            {
                Console.WriteLine("IsUserAdminAsync: Usuário não autenticado ou token vazio");
                return false;
            }

            try
            {
                var client = CreateAuthenticatedClient();
                var response = await client.GetAsync($"{API_BASE_URL}/api/users/current");
                
                if (response.IsSuccessStatusCode)
                {
                    var userDto = await response.Content.ReadFromJsonAsync<UserDto>();
                    if (userDto != null && userDto.Role == "Admin")
                    {
                        Console.WriteLine("IsUserAdminAsync: Usuário é Admin");
                        return true;
                    }
                }
                
                Console.WriteLine("IsUserAdminAsync: Usuário não é Admin ou erro na requisição");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar role do usuário: {ex.Message}");
                return false;
            }
        }

        public async Task SaveTokenAfterRenderAsync()
        {
            await SaveTokenToLocalStorage();
        }

        private async Task SaveTokenToLocalStorage()
        {
            if (!string.IsNullOrEmpty(_token))
            {
                try
                {
                    // Salvar token no localStorage sem timeout
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, _token);
                    Console.WriteLine("Token salvo com sucesso no localStorage");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao salvar token no localStorage: {ex.Message}");
                }
            }
        }
    }
}