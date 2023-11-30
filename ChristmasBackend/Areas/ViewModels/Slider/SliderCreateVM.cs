using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string SaleText { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
