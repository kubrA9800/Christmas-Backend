using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public BlogController(IBlogService blogService,
                              AppDbContext context,
                              IWebHostEnvironment env,
                              IMapper mapper)
        {
            _blogService = blogService;
            _context = context;
            _env = env;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            foreach (var photo in request.Photos)
            {

                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "File can be only image format");
                    return View(request);
                }

                if (!photo.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photos", "File size can be max 200 kb");
                    return View(request);
                }
            }

            List<BlogImage> newImages = new();

            foreach (var photo in request.Photos)
            {
                string fileName = $"{Guid.NewGuid()}-{photo.FileName}";

                string path = _env.GetFilePath("img/blog", fileName);

                await photo.SaveFileAsync(path);

                newImages.Add(new BlogImage { Image = fileName });
            }

            newImages.FirstOrDefault().IsMain = true;

            await _context.BlogImages.AddRangeAsync(newImages);

            await _context.Blogs.AddAsync(new Blog
            {
                Title = request.Title,
                Text = request.Text,
                Images = newImages
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _blogService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BlogVM blog = await _blogService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null) return BadRequest();

            BlogVM blog = await _blogService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            return View(_mapper.Map<BlogEditVM>(blog));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            if (id is null) return BadRequest();

            Blog blog = await _context.Blogs.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);

            if (blog is null) return NotFound();

            request.Images = blog.Images.ToList();

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            List<BlogImage> newImages = new();

            if (request.Photos != null)
            {
                foreach (var photo in request.Photos)
                {

                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File can be only image format");
                        return View(request);
                    }

                    if (!photo.CheckFilesize(200))
                    {
                        ModelState.AddModelError("Photos", "File size can be max 200 kb");
                        return View(request);
                    }
                }

                foreach (var photo in request.Photos)
                {
                    string fileName = $"{Guid.NewGuid()}-{photo.FileName}";

                    string path = _env.GetFilePath("img/blog", fileName);

                    await photo.SaveFileAsync(path);

                    newImages.Add(new BlogImage { Image = fileName });
                }

                await _context.BlogImages.AddRangeAsync(newImages);
            }

            newImages.AddRange(request.Images);

            _mapper.Map(request, blog);
            blog.Images = newImages;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlogImage(int id)
        {
            BlogImage image = await _context.BlogImages.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.BlogImages.Remove(image);

            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("img/blog", image.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return Ok();
        }

    }
}
