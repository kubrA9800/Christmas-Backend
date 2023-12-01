using ChristmasBackend.Areas.ViewModels.Setting;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Models;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetSettings();
        Task<List<Setting>> GetAllAsync();
        Task DeleteAsync(int id);
        Task CreateAsync(SettingCreateVM setting);
    }
}
