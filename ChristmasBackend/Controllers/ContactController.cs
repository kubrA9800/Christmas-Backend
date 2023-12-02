using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Contact;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.Controllers
{
    public class ContactController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IContactService _contactService;

        public ContactController( ISettingService settingService, IContactService contactService)
        {
            _settingService = settingService;
            _contactService = contactService;
        }
        public async Task<IActionResult> Index()
        {
            
            ContactVM contact = await _contactService.GetData();

            ContactPageVM model = new()
            {
               Contact= contact
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateMessage(ContactMessageCreateVM request)
        {

            await _contactService.CreateAsync(request);

            return RedirectToAction("Index", "Contact");

        }
    }
}
