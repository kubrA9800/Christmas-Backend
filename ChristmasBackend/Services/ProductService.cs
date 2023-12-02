using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Новая_папка;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace ChristmasBackend.Services
{
    public class ProductService:IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context,
                              IMapper mapper,
                              IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<ProductVM>> GetByTakeWithIncludes(int take)
        {
            return _mapper.Map<List<ProductVM>>(await _context.Products.Include(m => m.Category)
                                                                       .Include(m => m.Images)
                                                                       .Take(take).ToListAsync());
        }
       

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product dbproduct = await _context.Products.Include(m=>m.Images).FirstOrDefaultAsync(m => m.Id == id);


            _context.Products.Remove(dbproduct);
            await _context.SaveChangesAsync();


            foreach (var photo in dbproduct.Images)
            {

                string path = _env.GetFilePath("img/product", photo.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }


            }
        }

        public async Task<List<ProductVM>> ShowMoreOrLess(int take, int skip)
        {
            return _mapper.Map<List<ProductVM>>(await _context.Products.Include(m => m.Category)
                                                                       .Include(m => m.Images)
                                                                       .Skip(skip)
                                                                       .Take(take)
                                                                       .ToListAsync());
        }

        public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Category)
                                                             .Include(m => m.Images)
                                                             .Skip((page * take) - take)
                                                             .Take(take)
                                                             .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            return _mapper.Map<List<ProductVM>>(await _context.Products.Include(m => m.Category).Include(m => m.Images).ToListAsync());
        }

        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task<Product> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Products.Where(m => m.Id == id)
                                         .Include(m => m.Images)
                                         .Include(m => m.Category)
                                         .FirstOrDefaultAsync();
        }

        public async Task<List<ProductVM>> SearchAsync(string searchText)
        {
            var dbProducts = await _context.Products.Include(m => m.Images)
                                                 .Include(m => m.Category)
                                                 .OrderByDescending(m => m.Id)
                                                 .Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                                                 .Take(6)
                                                 .ToListAsync();

            return _mapper.Map<List<ProductVM>>(dbProducts);
        }
    }
}
