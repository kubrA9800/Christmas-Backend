using ChristmasBackend.Areas.ViewModels.Contact;
using ChristmasBackend.Areas.ViewModels.Setting;
using ChristmasBackend.Areas.ViewModels.Tag;
using ChristmasBackend.Models;
using ChristmasBackend.Services;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest;

            Setting setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            SettingEditVM model = new()
            {
                Key=setting.Key,
                Value = setting.Value
            };

            return View(model);
           

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,SettingEditVM setting)
        {
            if (id is null) return BadRequest();

            Setting dbSetting = await _settingService.GetByIdAsync((int)id);

            if (dbSetting is null) return NotFound();

            if (dbSetting.Value.Contains("png") || dbSetting.Value.Contains("jpeg") || dbSetting.Value.Contains("jpg"))
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(setting);
                //}
                setting.Value=dbSetting.Value;
                setting.Key = dbSetting.Key;
                

                if (setting.ImageValue is null)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (!setting.ImageValue.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(setting);
                }

                if (!setting.ImageValue.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(setting);
                }

            }


            await _settingService.EditAsync(setting);

            return RedirectToAction(nameof(Index));
        }

    }
}
