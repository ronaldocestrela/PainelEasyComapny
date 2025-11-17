using System.Net.Http.Json;
using WebCliente.Models;

namespace WebCliente.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IAuthService _authService;

        public CampaignService(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<CampaignListResponse?> GetAllCampaignsAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var response = await client.GetAsync($"/api/campaigns/list-all?pageNumber={pageNumber}&pageSize={pageSize}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CampaignListResponse>();
                }

                Console.WriteLine($"Erro ao buscar campaigns: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar campaigns: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateCampaignAsync(CreateCampaignRequest request)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var response = await client.PostAsJsonAsync("/api/campaigns/create", request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                Console.WriteLine($"Erro ao criar campaign: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar campaign: {ex.Message}");
                return false;
            }
        }
    }
}