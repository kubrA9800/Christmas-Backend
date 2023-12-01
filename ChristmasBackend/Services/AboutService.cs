using AutoMapper;
using ChristmasBackend.Areas.ViewModels.About;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public AboutService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AboutVM> GetAllAsync()
        {
            return _mapper.Map<AboutVM>(await _context.Abouts.FirstOrDefaultAsync());
        }

        public async Task EditAsync(AboutEditVM request)
        {
            string oldPath = _env.GetFilePath("img", request.Image);

            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string newPath = _env.GetFilePath("img", fileName);

            About dbAbout = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);

            _mapper.Map(request, dbAbout);

            dbAbout.Image = fileName;

            _context.Abouts.Update(dbAbout);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await request.Photo.SaveFileAsync(newPath);
        }

        public async Task<AboutVM> GetByIdAsync(int id)
        {
            var datas = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);
            AboutVM about = _mapper.Map<AboutVM>(datas);
            return about;
        }
    }
}
