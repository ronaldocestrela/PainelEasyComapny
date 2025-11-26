using System.Net.Http.Json;
using WebCliente.Models;

namespace WebCliente.Services
{
    public class ProjectService(IAuthService authService, IConfiguration configuration) : IProjectService
    {
        private readonly IAuthService _authService = authService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<ProjectDto>?> GetAllProjectsAsync()
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
                var response = await client.GetAsync($"{baseUrl}/api/projects/list-all");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ProjectDto>>();
                }

                Console.WriteLine($"Erro ao buscar projects: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar projects: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateProjectAsync(CreateProjectRequest request)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
                var response = await client.PostAsJsonAsync($"{baseUrl}/api/projects/create", request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                Console.WriteLine($"Erro ao criar projeto: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar projeto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddUserToProjectAsync(string userId, string projectId)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
                var response = await client.GetAsync($"{baseUrl}/api/projects/add-user?userId={userId}&projectId={projectId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                Console.WriteLine($"Erro ao adicionar usuário ao projeto: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar usuário ao projeto: {ex.Message}");
                return false;
            }
        }
    }
}