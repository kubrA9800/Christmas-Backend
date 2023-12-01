using ChristmasBackend.Areas.ViewModels.About;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IAboutService
    {
        Task <AboutVM> GetAllAsync();
        Task<AboutVM> GetByIdAsync(int id);
        Task EditAsync(AboutEditVM request);
    }
}
