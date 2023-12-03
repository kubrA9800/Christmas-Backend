using ChristmasBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Blog
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public List<BlogImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
