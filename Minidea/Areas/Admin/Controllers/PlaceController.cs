using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View(_context.AdvertismentPlaces.Where(p => p.IsActive == true).ToList());
        }

        public IActionResult Create(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Place = _context.AdvertismentPlaces.ToList();
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

            foreach (var p in placePhoto.AllPhotos)
            {
                if (p != null)
                {
                    if (p.ContentType.Contains("image/"))
                    {
                        string filename = await p.SaveAsync(_env.WebRootPath, "placePhoto");

                        AdvertismentPhoto img = new AdvertismentPhoto()
                        {
                            AdvertismentPlaceId = placePhoto.AdvertismentPlaceId,
                            AreaTitle = placePhoto.AreaTitle,
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

        [ActionName("Edit")]
        public IActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");
            var staticData = _context.AdvertismentPhotos.Where(p => p.AdvertismentPlaceId == id);

            return View(staticData);
        }

        [ActionName("EditPhotoLine")]
        public IActionResult EditPhotoLine(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");

            AdvertismentPhoto? advertismentPhoto = _context.AdvertismentPhotos.Find(id);

            return View(advertismentPhoto);
        }

        [ActionName("EditPhotoLine")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPhotoLine(AdvertismentPhoto photoLineViewModel)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            AdvertismentPhoto? newBackgroundImages = await _context.AdvertismentPhotos.FindAsync(photoLineViewModel.Id);

            if (newBackgroundImages == null) return View("Error");


            if (photoLineViewModel.Photo != null)
            {
                string computerPhoto = Path.Combine(_env.WebRootPath, "img", newBackgroundImages.PhotoURL);

                if (System.IO.File.Exists(computerPhoto))
                {
                    System.IO.File.Delete(computerPhoto);
                }

                string filename = await photoLineViewModel.Photo.SaveAsync(_env.WebRootPath, "placePhoto");
                photoLineViewModel.PhotoURL = filename;
                newBackgroundImages.PhotoURL = photoLineViewModel.PhotoURL;
            }

            newBackgroundImages.IsMain = true;
            newBackgroundImages.AreaTitle = photoLineViewModel.AreaTitle;

            AdvertismentPlace advertismentPlace =  _context.AdvertismentPlaces.Find(newBackgroundImages.AdvertismentPlaceId);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = advertismentPlace.Id });
        }


    }
}
