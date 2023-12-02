using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Areas.ViewModels.Subscribe;
using ChristmasBackend.Areas.ViewModels.Новая_папка;

namespace ChristmasBackend.ViewModels
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public List<AdvertVM> Adverts { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public List<BlogVM> Blogs { get; set; }
        public List<ProductVM> Products { get; set; }
        public SubscribeCreateVM Subscribe { get; set; }

    }
}
