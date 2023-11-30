using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
