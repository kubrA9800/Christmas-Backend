using ChristmasBackend.Areas.ViewModels.Slider;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
    }
}
