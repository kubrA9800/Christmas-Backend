using ChristmasBackend.Areas.ViewModels.Advert;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<List<AdvertVM>> GetAllAsync();
        Task CreateAsync(AdvertCreateVM request);
        Task DeleteAsync(int id);
        Task<AdvertVM> GetByIdAsync(int id);
        Task EditAsync(AdvertEditVM request);
    }
}
