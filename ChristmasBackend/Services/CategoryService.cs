using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
