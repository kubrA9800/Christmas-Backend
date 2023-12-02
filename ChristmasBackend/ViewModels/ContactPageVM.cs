using ChristmasBackend.Areas.ViewModels.Contact;

namespace ChristmasBackend.ViewModels
{
    public class ContactPageVM
    {
        public ContactVM Contact { get; set; }
        public ContactMessageCreateVM NewContact { get; set; }
    }
}
