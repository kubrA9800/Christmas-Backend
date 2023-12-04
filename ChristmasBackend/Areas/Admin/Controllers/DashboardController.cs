using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.Areas.Admin.Controllers
{
    
    public class DashboardController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
