namespace ChristmasBackend.Areas.ViewModels.Review
{
    public class ReviewVM
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
