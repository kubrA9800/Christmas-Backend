using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Tag
{
    public class TagEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
