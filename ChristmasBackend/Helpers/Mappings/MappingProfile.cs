using AutoMapper;
using ChristmasBackend.Areas.ViewModels.About;
using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Areas.ViewModels.Brand;
using ChristmasBackend.Areas.ViewModels.Category;
using ChristmasBackend.Areas.ViewModels.Contact;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Areas.ViewModels.Setting;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Areas.ViewModels.Subscribe;
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
            CreateMap<Blog, BlogVM>().ReverseMap();
            CreateMap<Blog, BlogEditVM>().ReverseMap();
            CreateMap<Blog, BlogCreateVM>().ReverseMap();
            CreateMap<BlogEditVM, BlogVM>().ReverseMap();


            CreateMap<Tag, TagVM>();
            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));
            CreateMap<About, AboutVM>();
            CreateMap<About, AboutEditVM>().ReverseMap();
            CreateMap<Team, TeamVM>();
            CreateMap<Brand, BrandVM>().ReverseMap();
            CreateMap<Brand, BrandCreateVM>().ReverseMap();
            CreateMap<Brand, BrandEditVM>().ReverseMap();
            CreateMap<Subscribe, SubscribeVM>().ReverseMap();
            CreateMap<SubscribeCreateVM, Subscribe>().ReverseMap();
            CreateMap<TeamCreateVM, Team>();
            CreateMap<TeamEditVM, Team>().ReverseMap();
            CreateMap<ContactInfo, ContactVM>().ReverseMap();
            CreateMap<ContactVM, ContactMessageVM>().ReverseMap();
            CreateMap<ContactMessage, ContactMessageVM>().ReverseMap();
            CreateMap<ContactMessageCreateVM, ContactMessage>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoEditVM>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoVM>().ReverseMap();

            CreateMap<Setting, SettingEditVM>().ReverseMap();
            CreateMap<Tag, TagCreateVM>().ReverseMap();
            CreateMap<Tag, TagEditVM>().ReverseMap();
            CreateMap<Category, CategoryEditVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, CategoryCreateVM>().ReverseMap();








        }
    }
}
