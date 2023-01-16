using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minidea.DAL;
using Minidea.ViewModels;
using Newtonsoft.Json;

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
            BlogAdsViewModel homePageViewModel = new BlogAdsViewModel
            {
                Places = _context.AdvertismentPlaces.Where(b => b.IsActive == true).OrderByDescending(b => b.Id).ToList(),
                Blogs = _context.Blogs.Include(p => p.Category).OrderByDescending(b => b.Id).Take(3).ToList()
            };

            ViewBag.TotalCount = _context.Blogs.Count();

            return View(homePageViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Load(int id)
        {
            var advertismentPhotos = await _context.AdvertismentPhotos
                .Where(p => p.IsMain == true && p.AdvertismentPlaceId == id).OrderByDescending(b => b.Id).ToListAsync();

            //  return PartialView("_PhotoPlacePartialView", advertismentPhotos);

            string value = string.Empty;

            value = JsonConvert.SerializeObject(advertismentPhotos, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
            );

            return Json(value);
        }
    }
}
