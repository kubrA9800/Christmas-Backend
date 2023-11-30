using ChristmasBackend.Services.Interfaces;
using ChristmasBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public FooterViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM model = _layoutService.GetFooterDatas();
            return await Task.FromResult(View(model));
        }
    }
}
