using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Areas.ViewModels.Новая_папка;
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
        private readonly IProductService _productService;
        public HomeController(AppDbContext context, 
                              ISliderService sliderService,
                              IAdvertService advertService,
                              IReviewService reviewService,
                              IBlogService blogService,
                              IProductService productService)
        {
            _context = context;
            _sliderService = sliderService;
            _advertService = advertService;
            _reviewService = reviewService;
            _blogService = blogService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {

            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<AdvertVM> adverts=await _advertService.GetAllAsync();
            List<ReviewVM> reviews= await _reviewService.GetAllAsync();
            List<BlogVM> blogs = await _blogService.GetAllAsync();
            List<ProductVM> products = await _productService.GetByTakeWithIncludes(3);
            HomeVM model= new()
            {
                Sliders = sliders,
                Adverts = adverts,
                Reviews = reviews,
                Blogs= blogs,
                Products= products
            };

            int productCount = await _productService.GetCountAsync();
            ViewBag.count = productCount;
            return View(model);
        }


        public async Task<IActionResult> LoadMore(int skipCount)
        {

            List<ProductVM> products = await _productService.ShowMoreOrLess(3, skipCount);

            return PartialView("_ProductPartial", products);
        }


    }
}