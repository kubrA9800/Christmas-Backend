using AutoMapper;
using ChristmasBackend.Areas.ViewModels.About;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AboutService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AboutVM> GetAllAsync()
        {
            return _mapper.Map<AboutVM>(await _context.Abouts.FirstOrDefaultAsync());
        }
    }
}
