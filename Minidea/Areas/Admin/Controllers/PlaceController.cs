using Microsoft.AspNetCore.Mvc;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlaceController : Controller
    {
        public IActionResult Places()
        {
            return View();
        }
    }
}
