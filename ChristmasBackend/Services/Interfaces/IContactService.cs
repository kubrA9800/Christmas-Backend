using ChristmasBackend.Areas.ViewModels.Contact;
using ChristmasBackend.Models;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactVM> GetData();
        Task<List<ContactMessageVM>> GetAllMessagesAsync();
        Task CreateAsync(ContactMessageCreateVM contact);
        Task DeleteAsync(int id);
        Task<ContactMessageVM> GetMessageByIdAsync(int id);
        Task<ContactInfoVM> GetInfoAsync();
        Task<ContactInfoVM> GetInfoByIdAsync(int id);
        Task EditInfoAsync(ContactInfoEditVM contact);
    }
}
