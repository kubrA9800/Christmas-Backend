using ChristmasBackend.Areas.ViewModels.Advert;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<List<AdvertVM>> GetAllAsync();
    }
}
