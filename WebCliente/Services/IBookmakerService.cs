using WebCliente.Models;

namespace WebCliente.Services
{
    public interface IBookmakerService
    {
        Task<List<BookmakerDto>?> GetAllBookmakersAsync();
    }
}