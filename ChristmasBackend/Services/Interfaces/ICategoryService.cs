using ChristmasBackend.Areas.ViewModels.Category;
using ChristmasBackend.Models;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ICategoryService
    {
       
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int id);
        Task<CategoryVM> GetByNameAsync(string name);
        Task CreateAsync(CategoryCreateVM category);
        Task Delete(int id);
        Task EditAsync(CategoryEditVM category);
    }
}
