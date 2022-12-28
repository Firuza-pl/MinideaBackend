using Microsoft.AspNetCore.Mvc;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        public IActionResult Blogs()
        {
            return View();
        }
    }
}
