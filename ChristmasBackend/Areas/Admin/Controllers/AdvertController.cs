using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IAdvertService _advertService;


        public AdvertController(AppDbContext context, IWebHostEnvironment env,
                                                      IMapper mapper,
                                                      IAdvertService advertService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _advertService = advertService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await _advertService.GetAllAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "File can be only image format");
                return View();
            }

            if (!request.Photo.CheckFilesize(200))
            {
                ModelState.AddModelError("Photos", "File size can  be max 100 kb");
                return View();
            }

            await _advertService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            AdvertVM advert = await _advertService.GetByIdAsync((int)id);

            if (advert is null) return NotFound();

            return View(advert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            await _advertService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Advert advert = await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id);

            if (advert is null) return NotFound();

            AdvertEditVM advertEditVM = _mapper.Map<AdvertEditVM>(advert);


            return View(advertEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AdvertEditVM request)
        {

            if (id is null) return BadRequest();

            Advert dbAdvert = await _context.Adverts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbAdvert is null) return NotFound();


            request.Image = dbAdvert.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo is null)
            {
                _mapper.Map(request, dbAdvert);
                _context.Adverts.Update(dbAdvert);
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


            await _advertService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
