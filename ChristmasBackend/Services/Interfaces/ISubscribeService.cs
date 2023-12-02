using ChristmasBackend.Areas.ViewModels.Subscribe;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<SubscribeVM>> GetAllAsync();
        Task DeleteAsync(int id);
        Task CreateAsync(SubscribeCreateVM subscribe);
    }
}
