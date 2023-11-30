using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Brand;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class BrandService : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BrandVM>> GetAlAsync()
        {
            return _mapper.Map<List<BrandVM>>(await _context.Brands.ToListAsync());
        }
    }
}
