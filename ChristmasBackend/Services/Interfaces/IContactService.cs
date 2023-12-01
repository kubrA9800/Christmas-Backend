using ChristmasBackend.Areas.ViewModels.Contact;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactVM> GetData();

    }
}
