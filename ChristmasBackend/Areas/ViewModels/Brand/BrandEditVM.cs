using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Brand
{
    public class BrandEditVM
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
    }
}
