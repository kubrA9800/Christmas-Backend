using ChristmasBackend.Areas.ViewModels.Tag;

namespace ChristmasBackend.Areas.ViewModels.Blog
{
    public class BlogDetailVM
    {
        public BlogVM Blog { get; set; }
        public List<TagVM> Tags { get; set; }
    }
}
