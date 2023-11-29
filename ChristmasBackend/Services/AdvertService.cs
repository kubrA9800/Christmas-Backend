using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AdvertService(IMapper mapper,AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<AdvertVM>> GetAllAsync()
        {
            return _mapper.Map<List<AdvertVM>>(await _context.Adverts.ToListAsync());
        }
    }
}
