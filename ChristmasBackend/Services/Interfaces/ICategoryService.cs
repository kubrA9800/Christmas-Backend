using ChristmasBackend.Models;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
    }
}
