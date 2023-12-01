using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Advert
{
    public class AdvertEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
