using ChristmasBackend.Areas.ViewModels.Новая_папка;
using ChristmasBackend.Models;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<ProductVM>> GetAllAsync();
        Task<List<ProductVM>> GetByTakeWithIncludes(int take);
        Task<Product> GetByIdWithIncludesAsync(int id);
        Task<List<ProductVM>> ShowMoreOrLess(int take, int skip);
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
        Task DeleteAsync(int id);
    }
}
