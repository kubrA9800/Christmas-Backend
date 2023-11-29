using ChristmasBackend.Models;

namespace ChristmasBackend.Areas.ViewModels.Blog
{
    public class BlogVM
    {
        public int Id { get; set; }
        public List<BlogImage>  Images{ get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
