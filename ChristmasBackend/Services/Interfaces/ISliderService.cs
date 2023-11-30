using ChristmasBackend.Areas.ViewModels.Slider;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<SliderVM> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateVM slider);
        Task DeleteAsync(int id);

        Task EditAsync(SliderEditVM slider);
    }
}
