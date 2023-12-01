using AutoMapper;
using ChristmasBackend.Areas.ViewModels.Contact;
using ChristmasBackend.Data;
using ChristmasBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Services
{
    public class ContactService:IContactService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public ContactService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ContactVM> GetData()
        {
            var info = await _context.ContactInfos.FirstOrDefaultAsync();

            return _mapper.Map<ContactVM>(info);
        }
    }
}
