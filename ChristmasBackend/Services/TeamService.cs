using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Team;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class TeamService : ITeamService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TeamVM>> GetAllAsync()
        {
            return _mapper.Map<List<TeamVM>>(await _context.Teams.ToListAsync());
        }
    }
}
