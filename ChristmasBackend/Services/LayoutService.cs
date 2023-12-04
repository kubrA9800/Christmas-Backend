using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels;

namespace ChristmasBackend.Services
{
    public class LayoutService:ILayoutService
    {
        private readonly ISettingService _settingService;
        private readonly ICartService _cartService;
        public LayoutService(ISettingService settingService, ICartService cartService)
        {
            _settingService = settingService;
            _cartService = cartService;
        }
        public HeaderVM GetHeaderDatas()
        {

            Dictionary<string, string> settingDatas = _settingService.GetSettings();
            int basketCount = _cartService.GetCount();
            
            return new HeaderVM
            {
                BasketCount = basketCount,
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
