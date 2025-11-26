using WebCliente.Models;

namespace WebCliente.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>?> GetAllProjectsAsync();
        Task<bool> CreateProjectAsync(CreateProjectRequest request);
        Task<bool> AddUserToProjectAsync(string userId, string projectId);
    }
}