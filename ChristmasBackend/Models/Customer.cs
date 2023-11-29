namespace ChristmasBackend.Models
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
