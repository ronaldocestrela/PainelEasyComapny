using System.Net.Http.Json;
using WebCliente.Models;

namespace WebCliente.Services
{
    public class BookmakerService(IAuthService authService) : IBookmakerService
    {
        private readonly IAuthService _authService = authService;

        public async Task<List<BookmakerDto>?> GetAllBookmakersAsync()
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var response = await client.GetAsync("/api/bookmakers/list-all");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<BookmakerDto>>();
                }

                Console.WriteLine($"Erro ao buscar bookmakers: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar bookmakers: {ex.Message}");
                return null;
            }
        }
    }
}