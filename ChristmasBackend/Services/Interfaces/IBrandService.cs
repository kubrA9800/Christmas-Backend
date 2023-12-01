using ChristmasBackend.Areas.ViewModels.Brand;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAlAsync();
        Task<BrandVM> GetByIdAsync(int id);
        Task CreateAsync(BrandCreateVM brand);
        Task DeleteAsync(int id);
        Task EditAsync(BrandEditVM brand);
    }
}
