using System.ComponentModel.DataAnnotations;

namespace ChristmasBackend.Areas.ViewModels.Setting
{
    public class SettingEditVM
    {
        public int Id { get; set; }
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
        public IFormFile ImageValue { get; set; }
    }
}
