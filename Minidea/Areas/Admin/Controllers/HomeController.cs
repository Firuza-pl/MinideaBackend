using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Minidea.DAL;
using Minidea.Extensions;
using Minidea.Models;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Db_MinideaContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public HomeController(Db_MinideaContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> siginInManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _siginInManager = siginInManager;
            _env = env;
        }
        public IActionResult Main()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_context.BackgroundImages.ToList());
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Active = "Home";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BackgroundImages backgroundImages)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!backgroundImages.Photo.IsImage())
            {
                ViewBag.Active = "Home";

                ModelState.AddModelError("Photo", "File type should be image");

                return View(backgroundImages);
            }

            string filename = await backgroundImages.Photo.SaveAsync(_env.WebRootPath, "backgroundImages");
            backgroundImages.PhotoURL = filename;


            BackgroundImages background = new BackgroundImages
            {
                PhotoURL = filename,
                IsActive = true
            };

            await _context.BackgroundImages.AddAsync(background);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Main));
        }


        [ActionName("Edit")]
        public IActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");
            BackgroundImages? backgroundImages = _context.BackgroundImages.Find(id);

            return View(backgroundImages);
        }


        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(BackgroundImages backgroundImages)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            BackgroundImages? newBackgroundImages = await _context.BackgroundImages.FindAsync(backgroundImages.Id);

            if (newBackgroundImages == null) return View("Error");


            if (backgroundImages.Photo != null)
            {
                string computerPhoto = Path.Combine(_env.WebRootPath, "img", newBackgroundImages.PhotoURL);

                if (System.IO.File.Exists(computerPhoto))
                {
                    System.IO.File.Delete(computerPhoto);
                }

                string filename = await backgroundImages.Photo.SaveAsync(_env.WebRootPath, "backgroundImages");
                backgroundImages.PhotoURL = filename;
                newBackgroundImages.PhotoURL = backgroundImages.PhotoURL;
            }

            newBackgroundImages.IsActive = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Main));
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");

            BackgroundImages? backgroundImages = await _context.BackgroundImages.FindAsync(id);

            if (backgroundImages == null) return View("Error");

            return View(backgroundImages);
        }


        [ActionName("Delete")]
        public async Task<IActionResult> DeleteGet(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");

            BackgroundImages? backgroundImages = await _context.BackgroundImages.FindAsync(id);

            if (backgroundImages == null) return View("Error");
            ViewBag.Active = "Home";

            return View(backgroundImages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            BackgroundImages? backgroundImages = await _context.BackgroundImages.FindAsync(id);

            backgroundImages.IsActive = false;

            await _context.SaveChangesAsync();
            ViewBag.Active = "Home";

            return RedirectToAction(nameof(Main));
        }
    }
}
