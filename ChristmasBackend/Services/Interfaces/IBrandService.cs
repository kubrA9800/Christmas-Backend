using ChristmasBackend.Areas.ViewModels.Brand;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAlAsync();
    }
}
