using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChristmasBackend.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;


        public CartController(AppDbContext context, ICartService cartService, IProductService productService)
        {
            _context = context;
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<CartVM> carts = _cartService.GetDatasFromCookie();
            List<CartDetailVM> cartDetailVM = new();

            foreach (var item in carts)
            {
                Product dbProduct = await _productService.GetByIdAsync(item.ProductId);

                cartDetailVM.Add(new CartDetailVM()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    Image = dbProduct.Images.FirstOrDefault(m => m.IsMain).Image,
                    Count = item.Count,
                    Total = dbProduct.Price * item.Count,
                });
            }
            return View(cartDetailVM);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProductFromBasket(int? id)
        {
            if (id == null) return BadRequest();

            var data = await _cartService.DeleteData((int)id);
            return Ok(data);
        }

      
    }
}
