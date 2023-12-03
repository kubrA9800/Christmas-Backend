using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Areas.ViewModels.Tag;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(AppDbContext context, IBlogService blogService, IMapper mapper)
        {
            _context = context;

            _blogService = blogService;
            _mapper = mapper;

        }
        public async Task<IActionResult> Index()
        {
            List<BlogVM> blogs = await _blogService.GetAllWithTakeAsync();
            List<Tag> tags = await _context.Tags.ToListAsync();
            var tag = _mapper.Map<List<TagVM>>(tags);



            BlogPageVM model = new()
            {
                Blogs = blogs,
                Tags = tag
            };
            return View(model);
        }
    }
}
