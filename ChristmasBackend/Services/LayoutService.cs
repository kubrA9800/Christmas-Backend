using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels;

namespace ChristmasBackend.Services
{
    public class LayoutService:ILayoutService
    {
        private readonly ISettingService _settingService;
        public LayoutService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public HeaderVM GetHeaderDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();


            return new HeaderVM
            {
                Logo = settingDatas["HeaderLogo"]
            };
        }

        public FooterVM GetFooterDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            return new FooterVM
            {
                Phone = settingDatas["Phone"],
                Address = settingDatas["Address"],
                Fax = settingDatas["Fax"],
                Email = settingDatas["Email"],
                Logo = settingDatas["FooterLogo"]
            };
        }
    }
}
