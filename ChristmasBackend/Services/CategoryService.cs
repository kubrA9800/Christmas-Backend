using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Category;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CategoryService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }


        public async Task EditAsync(CategoryEditVM category)
        {
            _context.Categories.Update(_mapper.Map<Category>(category));
            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryVM>> GetAllAsync()
        {
            return _mapper.Map<List<CategoryVM>>(await _context.Categories.ToListAsync());
        }

        public async Task<CategoryVM> GetByIdAsync(int id)
        {
            return _mapper.Map<CategoryVM>(await _context.Categories.FindAsync(id));
        }

        public async Task<CategoryVM> GetByNameAsync(string name)
        {
            return _mapper.Map<CategoryVM>(await _context.Categories.FirstOrDefaultAsync(m => m.Name.Trim() == name.Trim()));
        }


        public async Task CreateAsync(CategoryCreateVM category)
        {
            Category dbCategory = _mapper.Map<Category>(category);

            await _context.Categories.AddAsync(dbCategory);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Category dbCategory = await _context.Categories.Where(m => m.Id == id)
                                                           .Include(m => m.Products)
                                                           .ThenInclude(m => m.Images)
                                                           .FirstOrDefaultAsync();
            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();

            foreach (var product in dbCategory.Products)
            {
                foreach (var image in product.Images)
                {
                    string path = _env.GetFilePath("img/product/", image.Image);

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }

        }

    }
}
