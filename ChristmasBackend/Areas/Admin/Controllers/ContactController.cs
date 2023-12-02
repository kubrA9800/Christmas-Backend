using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Contact;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, AppDbContext context, IMapper mapper)
        {
            _contactService = contactService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MessageIndex()
        {
            return View(await _contactService.GetAllMessagesAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MessageDelete(int id)
        {
            await _contactService.DeleteAsync(id);
            return RedirectToAction(nameof(MessageIndex));
        }

        [HttpGet]
        public async Task<IActionResult> MessageDetail(int? id)
        {
            if (id is null) return BadRequest();

            ContactMessageVM contactMessage = await _contactService.GetMessageByIdAsync((int)id);

            if (contactMessage is null) return NotFound();

            return View(contactMessage);
        }


        [HttpGet]
        public async Task<IActionResult> InfoIndex()
        {
            return View(await _contactService.GetInfoAsync());
        }

        [HttpGet]
        public async Task<IActionResult> InfoDetail(int? id)
        {
            if (id is null) return BadRequest();

            ContactInfoVM contactInfo = await _contactService.GetInfoByIdAsync((int)id);

            if (contactInfo is null) return NotFound();

            return View(contactInfo);
        }

        [HttpGet]
        public async Task<IActionResult> InfoEdit(int? id)
        {
            if (id is null) return BadRequest();

            ContactInfoVM dbContactInfo = await _contactService.GetInfoByIdAsync((int)id);

            if (dbContactInfo is null) return NotFound();

            ContactInfoEditVM contactInfoEditVM = new()
            {
                Description = dbContactInfo.Description
            };


            return View(contactInfoEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InfoEdit(int? id, ContactInfoEditVM request)
        {

            if (id is null) return BadRequest();

            ContactInfoVM dbContactInfo = await _contactService.GetInfoByIdAsync((int)id);

            if (dbContactInfo is null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View();
            }

            await _contactService.EditInfoAsync(request);

            return RedirectToAction(nameof(InfoIndex));
        }
    }

}
