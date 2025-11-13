using System.Net.Http.Json;
using Microsoft.JSInterop;
using WebCliente.Models;

namespace WebCliente.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private string? _token;
        private const string API_BASE_URL = "http://localhost:5150";
        private const string TOKEN_KEY = "auth_token";

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_token);
        public string? Token => _token;

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{API_BASE_URL}/api/login", request);
                
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    
                    if (loginResponse != null)
                    {
                        _token = loginResponse.AccessToken;
                        
                        // Configurar o cabeçalho de autorização para futuras requisições
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                        
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
            
            // Remover o cabeçalho de autorização
            _httpClient.DefaultRequestHeaders.Authorization = null;
            
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
                    // Configurar o cabeçalho de autorização
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                    // Console.WriteLine($"Token recuperado do localStorage com sucesso. Token length: {_token.Length}");
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