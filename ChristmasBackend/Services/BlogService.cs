using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Blog;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public BlogService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
      

        public async Task<List<BlogVM>> GetAllAsync()
        {
            var blogs = await _context.Blogs.Include(m => m.Images).ToListAsync();

            return _mapper.Map<List<BlogVM>>(blogs);
        }

        public async Task<List<BlogVM>> GetAllWithTakeAsync()
        {
            var blogs = await _context.Blogs.Include(m => m.Images).Include(m => m.BlogTags).Take(3).ToListAsync();

            return _mapper.Map<List<BlogVM>>(blogs);
        }



        public async Task<BlogVM> GetByIdAsync(int id)
        {
            Blog blog = await _context.Blogs.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<BlogVM>(blog);
        }

        public async Task DeleteAsync(int id)
        {
            Blog dbBlog = await _context.Blogs.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);


            _context.Blogs.Remove(dbBlog);
            await _context.SaveChangesAsync();


            foreach (var photo in dbBlog.Images)
            {

                string path = _env.GetFilePath("img/blog", photo.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

            }
        }
    }
}
