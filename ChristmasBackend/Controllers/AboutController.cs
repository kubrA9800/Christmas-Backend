using ChristmasBackend.Areas.ViewModels.About;
using ChristmasBackend.Areas.ViewModels.Brand;
using ChristmasBackend.Areas.ViewModels.Team;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly ITeamService _teamService;
        private readonly IBrandService _brandService;
        public AboutController(IAboutService aboutService, ITeamService teamService, IBrandService brandService)
        {
            _aboutService = aboutService; 
            _teamService = teamService;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            AboutVM about= await _aboutService.GetAllAsync();
            List<TeamVM> teams = await _teamService.GetAllAsync();
            List<BrandVM> brands =await _brandService.GetAlAsync();
            AboutPageVM model = new()
            {
                About = about,
                Teams = teams,
                Brands=brands
            };

            return View(model);
        }
    }
}
