using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Tag
{
    public class TagCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
