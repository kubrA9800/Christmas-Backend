using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Models;

namespace ChristmasBackend.Helpers.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Slider, SliderVM>();
            CreateMap<Advert, AdvertVM>();
            CreateMap<Review, ReviewVM>().ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Customer.Image));
            CreateMap<Blog, BlogVM>().ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));
        }
    }
}
