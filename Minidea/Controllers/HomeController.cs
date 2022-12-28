using Microsoft.AspNetCore.Mvc;

namespace Minidea.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
