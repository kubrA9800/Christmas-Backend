using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Team
{
    public class TeamEditVM
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
