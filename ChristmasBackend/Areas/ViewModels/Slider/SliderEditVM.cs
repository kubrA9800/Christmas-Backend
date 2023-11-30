using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Slider
{
    public class SliderEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SaleText { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
