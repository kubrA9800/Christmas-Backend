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
        public IActionResult DeleteDataFromBasket(int? id)
        {
            if (id is null) return BadRequest();
            _cartService.DeleteData((int)id);
            List<CartVM> baskets = _cartService.GetDatasFromCookie();
            return Ok(baskets.Count);
        }

        [HttpPost]
        public IActionResult IncrementProductCount(int? id)
        {
            if (id is null) return BadRequest();
            var baskets = JsonConvert.DeserializeObject<List<CartVM>>(Request.Cookies["basket"]);
            var count = baskets.FirstOrDefault(b => b.ProductId == id).Count++;

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));

            return Ok(count);
        }

        [HttpPost]
        public IActionResult DecrementProductCount(int? id)
        {
            if (id is null) return BadRequest();
            var baskets = JsonConvert.DeserializeObject<List<CartVM>>(Request.Cookies["basket"]);
            var product = (baskets.FirstOrDefault(b => b.ProductId == id));
            if (product.Count == 1)
            {
                return Ok();
            }
            var count = product.Count--;
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));

            return Ok(count);
        }
    }
}
