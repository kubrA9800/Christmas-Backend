using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.About
{
    public class AboutEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        public string Video { get; set; }
        public string Image { get; set; }
    }
}
