using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Models;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllAsync();
        Task<BlogVM> GetByIdAsync(int id);
    }
}
