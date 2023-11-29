using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BlogService(AppDbContext context, IMapper mapper)
        {
            _context= context;
            _mapper= mapper;
        }
        public async Task<List<BlogVM>> GetAllAsync()
        {
            return  _mapper.Map<List<BlogVM>>(await _context.Blogs.Include(m=>m.Images).Take(3).ToListAsync());
        }
    }
}
