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
    public class BlogDetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        public BlogDetailController(AppDbContext context,IBlogService blogService, IMapper mapper)
        {
            _context = context;
            _blogService = blogService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();


            BlogVM blog = await _blogService.GetByIdAsync((int)id);
            List<Tag> tags = await _context.Tags.ToListAsync();
            var tag= _mapper.Map<List<TagVM>>(tags);
            BlogDetailVM model = new()
            {
                Blog = blog,
                Tags = tag
            };
            return View(model);
        }
    }
}
