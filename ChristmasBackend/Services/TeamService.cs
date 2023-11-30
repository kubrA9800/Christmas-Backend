using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Team;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class TeamService : ITeamService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamService(AppDbContext context, 
                           IMapper mapper,
                           IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;

        }

        public async Task Delete(int id)
        {
            Team team= await _context.Teams.FirstOrDefaultAsync(m=>m.Id==id);

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();


            string path = _env.GetFilePath("img/team", team.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

      

        public async Task<List<TeamVM>> GetAllAsync()
        {
            return _mapper.Map<List<TeamVM>>(await _context.Teams.ToListAsync());
        }

        public async Task<TeamVM> GetByIdAsync(int id)
        {
            return _mapper.Map<TeamVM>(await _context.Teams.Where(m=>m.Id==id).FirstOrDefaultAsync());
        }


        public async Task EditAsync(TeamEditVM team)
        {
            string oldPath = _env.GetFilePath("img/team", team.Image);
            string fileName = $"{Guid.NewGuid()}-{team.Photo.FileName}";
            string newPath = _env.GetFilePath("img/team", fileName);
            Team dbteam = await _context.Teams.FirstOrDefaultAsync(m => m.Id == team.Id);

            dbteam.Image = fileName;
            dbteam.FullName=team.FullName;
            dbteam.Position = team.Position;
    

            _context.Teams.Update(dbteam);
            await _context.SaveChangesAsync();

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await team.Photo.SaveFileAsync(newPath);

        }
    }
}
