using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Areas.ViewModels.Subscribe;
using ChristmasBackend.Areas.ViewModels.Новая_папка;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels;
using ChristmasBackend.ViewModels.Cart;
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
        private readonly ISubscribeService _subscribeService;
        private readonly ICartService _cartService;
        public HomeController(AppDbContext context, 
                              ISliderService sliderService,
                              IAdvertService advertService,
                              IReviewService reviewService,
                              IBlogService blogService,
                              IProductService productService,
                              ISubscribeService subscribeService,
                              ICartService cartService)
        {
            _context = context;
            _sliderService = sliderService;
            _advertService = advertService;
            _reviewService = reviewService;
            _blogService = blogService;
            _productService = productService;
            _subscribeService = subscribeService;
            _cartService = cartService;

        }

        public async Task<IActionResult> Index()
        {

            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<AdvertVM> adverts=await _advertService.GetAllAsync();
            List<ReviewVM> reviews = await _reviewService.GetAllWithIncludeAsync();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubscribe(SubscribeCreateVM subscribe)
        {

            await _subscribeService.CreateAsync(subscribe);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetByIdAsync((int)id);

            if (dbProduct == null) return NotFound();

            List<CartVM> carts = _cartService.GetDatasFromCookie();

            CartVM existProduct = carts.FirstOrDefault(p => p.ProductId == id);

            _cartService.SetDatasToCookie(carts, dbProduct, existProduct);

            int cartCount = carts.Count;

            return Ok(cartCount);
        }


    }
}