using Microsoft.AspNetCore.Mvc;

namespace Minidea.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blogs()
        {
            return View();
        }
        public IActionResult SingleBlog()
        {
            return View();
        }
    }
}
