using Microsoft.Build.Evaluation;

namespace ChristmasBackend.Areas.ViewModels.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
