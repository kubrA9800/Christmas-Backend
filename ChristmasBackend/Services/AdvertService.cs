using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Advert;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public AdvertService(IMapper mapper,
                             AppDbContext context, 
                             IWebHostEnvironment env)
        {
            _mapper = mapper;
            _context = context;
            _env = env;

        }
        public async Task<List<AdvertVM>> GetAllAsync()
        {
            return _mapper.Map<List<AdvertVM>>(await _context.Adverts.ToListAsync());
        }



        public async Task CreateAsync(AdvertCreateVM request)
        {
            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string path = _env.GetFilePath("img/banner", fileName); 

            Advert entity = _mapper.Map<Advert>(request);

            entity.Image = fileName;

            await _context.Adverts.AddAsync(entity);
            await _context.SaveChangesAsync();


            await request.Photo.SaveFileAsync(path);
        }

        public async Task DeleteAsync(int id)
        {
            Advert dbAdvert = await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id);


            _context.Adverts.Remove(dbAdvert);
            await _context.SaveChangesAsync();


            string path = _env.GetFilePath("img/banner", dbAdvert.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(AdvertEditVM request)
        {
            string oldPath = _env.GetFilePath("img/banner", request.Image);

            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string newPath = _env.GetFilePath("img/banner", fileName);

            Advert dbAdvert = await _context.Adverts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbAdvert);

            dbAdvert.Image = fileName;

            _context.Adverts.Update(dbAdvert);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await request.Photo.SaveFileAsync(newPath);
        }

        public async Task<AdvertVM> GetByIdAsync(int id)
        {
            
            return _mapper.Map<AdvertVM>(await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id));
        }
    }
}
