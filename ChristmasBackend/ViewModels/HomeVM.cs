using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Areas.ViewModels.Slider;

namespace ChristmasBackend.ViewModels
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public List<AdvertVM> Adverts { get; set; }
        public List<ReviewVM> Reviews { get; set; }


    }
}
