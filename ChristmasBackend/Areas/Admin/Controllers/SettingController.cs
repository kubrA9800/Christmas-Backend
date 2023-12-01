using ChristmasBackend.Areas.ViewModels.Contact;
using ChristmasBackend.Services;
using ChristmasBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

namespace ChristmasBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            return View(await _settingService.GetAllAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _settingService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
