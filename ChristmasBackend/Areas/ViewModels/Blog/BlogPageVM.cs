using ChristmasBackend.Areas.ViewModels.Tag;

namespace ChristmasBackend.Areas.ViewModels.Blog
{
    public class BlogPageVM
    {
        public List<BlogVM> Blogs { get; set; }
        public List<TagVM> Tags { get; set; }
    }
}
