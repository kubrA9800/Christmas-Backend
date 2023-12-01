using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Brand
{
    public class BrandCreateVM
    {
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
