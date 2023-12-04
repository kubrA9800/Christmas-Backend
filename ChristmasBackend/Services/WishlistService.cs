using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels.Wishlist;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ChristmasBackend.Services
{
    public class WishlistService : IWishListService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        public WishlistService(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public void DeleteData(int? id)
        {
            var wishlists = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            var deletedProduct = wishlists.FirstOrDefault(b => b.ProductId == id);
            wishlists.Remove(deletedProduct);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlists));
        }

        public async Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? wishlistId)
        {
            return await _context.WishlistProducts.Where(c => c.WishlistId == wishlistId).ToListAsync();
        }

        public async Task<Wishlist> GetByUserIdAsync(string userId)
        {
            return await _context.Wishlists.Include(c => c.WishlistProducts).FirstOrDefaultAsync(c => c.AppUserId == userId);
        }

        public List<WishlistVM> GetDatasFromCookie()
        {
            List<WishlistVM> wishlists;

            if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlists = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlists = new List<WishlistVM>();
            }
            return wishlists;
        }

        public void SetDatasToCookie(List<WishlistVM> wishlists, Product dbProduct, WishlistVM existProduct)
        {
            if (existProduct == null)
            {
                wishlists.Add(new WishlistVM
                {
                    ProductId = dbProduct.Id,
                });
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlists));
        }
    }
}
