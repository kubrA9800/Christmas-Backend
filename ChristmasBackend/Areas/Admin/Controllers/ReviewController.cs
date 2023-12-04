using ChristmasBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.Areas.Admin.Controllers
{
    
    public class ReviewController : MainController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _reviewService.GetAllWithIncludeAsync());
        }

        [HttpGet]

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _reviewService.GetByIdWithIncludeAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            await _reviewService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));

        }
    }
}
