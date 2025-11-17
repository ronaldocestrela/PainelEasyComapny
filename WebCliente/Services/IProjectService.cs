using WebCliente.Models;

namespace WebCliente.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>?> GetAllProjectsAsync();
        Task<bool> AddUserToProjectAsync(string userId, string projectId);
    }
}