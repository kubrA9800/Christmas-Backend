using ChristmasBackend.Areas.ViewModels.About;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IAboutService
    {
        Task <AboutVM> GetAllAsync();
    }
}
