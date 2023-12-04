using ChristmasBackend.Areas.ViewModels.Новая_папка;
using ChristmasBackend.Data;
using ChristmasBackend.Helpers;
using ChristmasBackend.Models;
using ChristmasBackend.Services;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels.Wishlist;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ChristmasBackend.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWishListService _wishListService; 


        public ShopController(IProductService productService, AppDbContext context, UserManager<AppUser> userManager, IWishListService wishListService)
        {
            _productService = productService;
            _context = context;
            _userManager = userManager;
            _wishListService= wishListService;
        }

        
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        public async Task<IActionResult> Search(string searchText)
        {
            if (searchText == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await _productService.SearchAsync(searchText));
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(WishlistDetailVM wishlistAdd)
        {
            if (!ModelState.IsValid) return BadRequest(wishlistAdd);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var product = await _context.Products.FindAsync(wishlistAdd.Id);
            if (product == null) return NotFound();

            var wishlist = await _context.Wishlists.Include(m => m.WishlistProducts).FirstOrDefaultAsync(m => m.AppUserId == user.Id);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    AppUserId = user.Id
                };

                await _context.Wishlists.AddAsync(wishlist);
                await _context.SaveChangesAsync();
            }
            var wishlistProduct = await _context.WishlistProducts
                .FirstOrDefaultAsync(bp => bp.ProductId == product.Id && bp.WishlistId == wishlist.Id);

            List<WishlistProduct> wishlistProducts = await _context.WishlistProducts.Where(m => m.WishlistId == wishlist.Id).ToListAsync();

            foreach (var item in wishlistProducts)
            {
                if (item.ProductId != product.Id)
                {
                    wishlistProduct = new WishlistProduct
                    {
                        WishlistId = wishlist.Id,
                        ProductId = product.Id,

                    };
                }
            }


            await _context.WishlistProducts.AddAsync(wishlistProduct);

            await _context.SaveChangesAsync();


            List<WishlistVM> wishlists = new();

            if (wishlist is not null)
            {
                wishlistProducts = await _context.WishlistProducts.Where(m => m.WishlistId == wishlist.Id).ToListAsync();
                foreach (var item in wishlistProducts)
                {
                    wishlists.Add(new WishlistVM
                    {
                        ProductId = item.ProductId,
                    });
                }
                Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlists));
            }

            return Ok(_wishListService.GetDatasFromCookie().Count());
        }

    }
}

