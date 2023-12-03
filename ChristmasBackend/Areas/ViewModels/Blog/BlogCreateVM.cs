using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
