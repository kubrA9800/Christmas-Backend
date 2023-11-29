namespace ChristmasBackend.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public List<BlogImage> Images { get; set; }
        public List<BlogTag> BlogTags { get; set; }
    }
}
