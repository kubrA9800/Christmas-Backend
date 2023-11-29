using ChristmasBackend.Areas.ViewModels.Blog;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllAsync();
    }
}
