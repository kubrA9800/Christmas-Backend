using ChristmasBackend.Areas.ViewModels.About;
using ChristmasBackend.Areas.ViewModels.Brand;
using ChristmasBackend.Areas.ViewModels.Team;
using ChristmasBackend.Models;

namespace ChristmasBackend.ViewModels
{
    public class AboutPageVM
    {
        public AboutVM About { get; set; }
        public List<TeamVM> Teams { get; set; }
        public List<BrandVM> Brands { get; set; }
    }
}
