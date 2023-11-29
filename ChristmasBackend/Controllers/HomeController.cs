using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChristmasBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IAdvertService _advertService;
        private IReviewService _reviewService;
        private readonly IBlogService _blogService;
        public HomeController(AppDbContext context, 
                              ISliderService sliderService,
                              IAdvertService advertService,
                              IReviewService reviewService,
                              IBlogService blogService)
        {
            _context = context;
            _sliderService = sliderService;
            _advertService = advertService;
            _reviewService = reviewService;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {

            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<AdvertVM> adverts=await _advertService.GetAllAsync();
            List<ReviewVM> reviews= await _reviewService.GetAllAsync();
            List<BlogVM> blogs = await _blogService.GetAllAsync();
            HomeVM model= new()
            {
                Sliders = sliders,
                Adverts = adverts,
                Reviews = reviews,
                Blogs= blogs
            };
            return View(model);
        }

       
    }
}