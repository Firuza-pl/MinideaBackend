using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Minidea.DAL;
using Minidea.Extensions;
using Minidea.Models;
using Minidea.ViewModels;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PlaceController : Controller
    {
        private readonly Db_MinideaContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public PlaceController(Db_MinideaContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> siginInManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _siginInManager = siginInManager;
            _env = env;
        }

        public IActionResult Places()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

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
        public async Task<IActionResult> Create(PlacePhotoVIewModel placePhoto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (placePhoto.AllPhotos == null)
            {
                ViewBag.Active = "Home";
                ModelState.AddModelError("AllPhotos", "Xahiş olunur şəkil əlavə edin.");

                return View(placePhoto);
            }

            AdvertismentPlace place = new AdvertismentPlace()
            {
                BigTitle = placePhoto.BigTitle,
                IsActive=true
            };


            await _context.AdvertismentPlaces.AddAsync(place);

            await _context.SaveChangesAsync();

            foreach (var p in placePhoto.AllPhotos)
            {
                if (p != null)
                {
                    if (p.ContentType.Contains("image/"))
                    {
                        string filename = await p.SaveAsync(_env.WebRootPath, "placePhoto");

                        AdvertismentPhoto img = new AdvertismentPhoto()
                        {
                            AdvertismentPlaceId = place.Id,
                            AreaTitle=placePhoto.AreaTitle,
                            PhotoURL = filename,
                            IsMain = true

                    };

                        await _context.AdvertismentPhotos.AddAsync(img);
                    }
                }
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Places");
        }


    }
}
