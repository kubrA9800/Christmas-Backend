namespace ChristmasBackend.Models
{
    public class Cart:BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<CartProduct> CartProducts { get; set; }
    }
}
