using Microsoft.AspNetCore.Mvc;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
    }
}
