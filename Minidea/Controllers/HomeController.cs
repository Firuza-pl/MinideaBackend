using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minidea.DAL;
using Minidea.ViewModels;

namespace Minidea.Controllers
{
    public class HomeController : Controller
    {
        private readonly Db_MinideaContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        public HomeController(Db_MinideaContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            ViewBag.Active = "Home";

            return View();
        }
    }
}
