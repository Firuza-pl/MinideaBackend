using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Minidea.DAL;
using Minidea.Extensions;
using Minidea.Models;
using System.Drawing;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GeneralController : Controller
    {
        private readonly Db_MinideaContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;


        public GeneralController(Db_MinideaContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> siginInManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _siginInManager = siginInManager;
            _env = env;
        }

        public IActionResult Generals()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
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
        public async Task<IActionResult> Create(StaticData staticData)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (staticData.Photo == null) { 
                ModelState.AddModelError("Photo", "Xahiş edirik şəkil yükləyin.");
                return View(staticData);
            }

            if (!staticData.Photo.IsImage())
            {
                ViewBag.Active = "Home";

                ModelState.AddModelError("Photo", "File type should be image");
                return View(staticData);
            }

            string filename = await staticData.Photo.SaveAsync(_env.WebRootPath, "staticdata");
            staticData.PhotoURL = filename;

            StaticData newStaticData = new StaticData
            {
                PhoneOne = staticData.PhoneOne,
                MobileTwo = staticData.MobileTwo,
                MobileThree = staticData.MobileThree,
                Email = staticData.Email,
                FacebookLink = staticData.FacebookLink,
                InstagramLink = staticData.InstagramLink,
                LinkedinLink = staticData.LinkedinLink,
                PhotoURL = filename,
                //IsActive = true
            };

            await _context.StaticDatas.AddAsync(newStaticData);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Generals));
        }

        [ActionName("Edit")]
        public IActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }


        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // new { id = brand.Id }
        }


        public IActionResult Delete()
        {
            return View();
        }
    }
}
