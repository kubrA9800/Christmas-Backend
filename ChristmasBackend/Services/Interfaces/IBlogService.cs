using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Models;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllWithTakeAsync();
        Task<BlogVM> GetByIdAsync(int id);
        Task<List<BlogVM>> GetAllAsync();
        Task DeleteAsync(int id);

    }
}
