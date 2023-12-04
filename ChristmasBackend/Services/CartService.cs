using ChristmasBackend.Data;
using ChristmasBackend.Helpers.Responses;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels.Cart;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ChristmasBackend.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public CartService(IHttpContextAccessor httpContextAccessor, 
                           AppDbContext context,
                           IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _productService = productService;

        }
        public async Task<DeleteBasketResponses> DeleteData(int? id)
        {
            List<decimal> grandTotal = new();
            var basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            var deletedProduct = basket.FirstOrDefault(b => b.ProductId == id);
            basket.Remove(deletedProduct);

            foreach (var item in basket)
            {
                var product = await _productService.GetByIdAsync(item.ProductId);

                decimal productPrice = product.Price;

                decimal total = item.Count * productPrice;

                grandTotal.Add(total);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return new DeleteBasketResponses
            {
                Count = basket.Sum(m => m.Count),
                GrandTotal = grandTotal.Sum()
            };


        }


        public async Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId)
        {
            return await _context.CartProducts.Where(c => c.CartId == cartId).ToListAsync();
        }

        public async Task<Cart> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.AppUserId == userId);
        }

        public List<CartVM> GetDatasFromCookie()
        {
            List<CartVM> carts;
            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                carts = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                carts = new List<CartVM>();
            }
            return carts;
        }

        public void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct)
        {
            if (existProduct == null)
            {
                carts.Add(new CartVM
                {
                    ProductId = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(carts));
        }

        public int GetCount()
        {
            List<CartVM> basket;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<CartVM>();
            }
            return basket.Sum(m => m.Count);

        }

      
    }
}
