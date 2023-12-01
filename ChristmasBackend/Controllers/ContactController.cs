using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Contact;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
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
            Dictionary<string, string> settingDatas = _settingService.GetSettings();
            ContactVM contact = await _contactService.GetData();

            ContactVM model = new()
            {
                Description = contact.Description,
                Phone = settingDatas["Phone"],
                Address = settingDatas["Address"],
                Email = settingDatas["Email"]
            };
            return View(model);
        }
    }
}
