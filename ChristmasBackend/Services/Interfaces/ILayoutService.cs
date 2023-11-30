using ChristmasBackend.ViewModels;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ILayoutService
    {
        HeaderVM GetHeaderDatas();
        FooterVM GetFooterDatas();
    }
}
