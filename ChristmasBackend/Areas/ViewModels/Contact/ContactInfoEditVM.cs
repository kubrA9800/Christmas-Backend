using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Contact
{
    public class ContactInfoEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
