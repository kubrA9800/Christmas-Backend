using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class ReviewServices : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ReviewServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ReviewVM>> GetAllAsync()
        {
            return _mapper.Map<List<ReviewVM>>(await _context.Reviews.Include(m => m.Customer).ToListAsync());
        }

        public async Task DeleteAsync(int id)
        {
            Review review = await _context.Reviews.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();


        }

        public async Task<List<ReviewVM>> GetAllWithIncludeAsync()
        {
            return _mapper.Map<List<ReviewVM>>(await _context.Reviews.Include(m => m.Customer).ToListAsync());
        }

        public async Task<ReviewVM> GetByIdWithIncludeAsync(int id)
        {
            return _mapper.Map<ReviewVM>(await _context.Reviews.Include(m => m.Customer).FirstOrDefaultAsync(m => m.Id == id));
        }
    }
}
