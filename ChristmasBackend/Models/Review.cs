namespace ChristmasBackend.Models
{
    public class Review:BaseEntity
    {
        public string Text { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
