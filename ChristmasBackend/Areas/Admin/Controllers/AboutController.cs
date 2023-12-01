using AutoMapper;
using ChristmasBackend.Areas.ViewModels.About;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IAboutService _aboutService;


        public AboutController(AppDbContext context, IWebHostEnvironment env,
        IMapper mapper,
                                                      IAboutService aboutService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _aboutService = aboutService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _aboutService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            AboutVM about = await _aboutService.GetByIdAsync((int)id);

            if (about is null) return NotFound();

            return View(about);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            About about = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);

            if (about is null) return NotFound();

            AboutEditVM aboutEditVM = _mapper.Map<AboutEditVM>(about);


            return View(aboutEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {

            if (id is null) return BadRequest();

            About dbAbout = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbAbout is null) return NotFound();


            request.Image = dbAbout.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo is null)
            {
                _mapper.Map(request, dbAbout);
                _context.Abouts.Update(dbAbout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }

                if (!request.Photo.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can  be max 200 kb");
                    return View(request);
                }
            }


            await _aboutService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
