using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Setting;
using ChristmasBackend.Areas.ViewModels.Slider;
using ChristmasBackend.Data;
using ChristmasBackend.Models;
using ChristmasBackend.Services.Interfaces;
using GreenyBackend.Helpers.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public SettingService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       

        public async Task DeleteAsync(int id)
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);

            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();

            if (setting.Value.Contains("png") || setting.Value.Contains("jpeg")|| setting.Value.Contains("jpg"))
            {
                string path = _env.GetFilePath("img", setting.Value);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

        }




        public async Task<List<Setting>> GetAllAsync()
        {
            return  await _context.Settings.ToListAsync();
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.Where(m => !m.SoftDeleted)
                                         .AsEnumerable()
                                         .ToDictionary(m => m.Key, m => m.Value);
        }



        public async Task CreateAsync(SettingCreateVM setting)
        {
            string fileName = $"{Guid.NewGuid()}-{setting.ImageValue.FileName}";

            string path = _env.GetFilePath("img", fileName);

            var data = _mapper.Map<Slider>(setting);

            data.Image = fileName;

            await _context.AddAsync(data);

            await _context.SaveChangesAsync();

            await setting.ImageValue.SaveFileAsync(path);

        }

    }
}
