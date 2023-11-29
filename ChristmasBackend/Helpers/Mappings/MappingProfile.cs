using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Models;

namespace ChristmasBackend.Helpers.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Slider, SliderVM>();
        }
    }
}
