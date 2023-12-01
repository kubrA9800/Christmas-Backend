using AutoMapper;
using ChristmasBackend.Areas.ViewModels.About;
using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Areas.ViewModels.Brand;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Areas.ViewModels.Tag;
using ChristmasBackend.Areas.ViewModels.Team;
using ChristmasBackend.Areas.ViewModels.Новая_папка;
using ChristmasBackend.Models;

namespace ChristmasBackend.Helpers.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Slider, SliderVM>();
            CreateMap<Slider, SliderCreateVM>().ReverseMap();
            CreateMap<Advert, AdvertVM>();
            CreateMap<Advert, AdvertCreateVM>().ReverseMap();
            CreateMap<Advert, AdvertEditVM>().ReverseMap();
            CreateMap<Review, ReviewVM>().ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Customer.Image));
            CreateMap<Blog, BlogVM>();
            CreateMap<Tag, TagVM>();
            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));
            CreateMap<About, AboutVM>();
            CreateMap<Team, TeamVM>();
            CreateMap<Brand, BrandVM>();
            CreateMap<TeamCreateVM, Team>();


        }
    }
}
