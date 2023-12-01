using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Advert
{
    public class AdvertCreateVM
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
