using ChristmasBackend.Areas.ViewModels.Новая_папка;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetByTakeWithIncludes(int take);
        Task<List<ProductVM>> ShowMoreOrLess(int take, int skip);
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
    }
}
