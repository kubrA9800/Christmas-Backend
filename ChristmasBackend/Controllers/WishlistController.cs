using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels.Wishlist;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishListService _wishListService;
        private readonly IProductService _productService;
        public WishlistController(IWishListService wishListService, IProductService productService)
        {
            _wishListService = wishListService;
            _productService = productService;

        }
        public async Task<IActionResult> Index()
        {

            List<WishlistVM> wishlists = _wishListService.GetDatasFromCookie();
            List<WishlistDetailVM> wishlistDetails = new();

            foreach (var item in wishlists)
            {
                Product dbProduct = await _productService.GetByIdAsync(item.ProductId);

                wishlistDetails.Add(new WishlistDetailVM
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    Image = dbProduct.Images.FirstOrDefault(m => m.IsMain).Image
                });
            }


            return View(wishlistDetails);
        }
    }
}
