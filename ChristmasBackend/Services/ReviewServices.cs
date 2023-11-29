using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Review;
using ChristmasBackend.Data;
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
    }
}
