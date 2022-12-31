﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Minidea.DAL;
using Minidea.Extensions;
using Minidea.Models;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View(_context.StaticDatas.Where(p => p.IsActive == true).ToList());
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

            if (staticData.Photo == null)
            {
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
                IsActive = true
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

            if (id == null) return View("Error");
            StaticData? staticData = _context.StaticDatas.Find(id);

            return View(staticData);
        }


        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(StaticData staticData)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            StaticData? newStaticData = await _context.StaticDatas.FindAsync(staticData.Id);

            if (newStaticData == null) return View("Error");


            if (staticData.Photo != null)
            {
                string computerPhoto = Path.Combine(_env.WebRootPath, "img", newStaticData.PhotoURL);

                if (System.IO.File.Exists(computerPhoto))
                {
                    System.IO.File.Delete(computerPhoto);
                }

                string filename = await staticData.Photo.SaveAsync(_env.WebRootPath, "staticdata");
                staticData.PhotoURL = filename;
                newStaticData.PhotoURL = staticData.PhotoURL;
            }

            newStaticData.PhoneOne = staticData.PhoneOne;
            newStaticData.MobileTwo = staticData.MobileTwo;
            newStaticData.MobileThree = staticData.MobileThree;
            newStaticData.Email = staticData.Email;
            newStaticData.FacebookLink = staticData.FacebookLink;
            newStaticData.InstagramLink = staticData.InstagramLink;
            newStaticData.LinkedinLink = staticData.LinkedinLink;
            newStaticData.IsActive = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Generals));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");

            StaticData? staticData = await _context.StaticDatas.FindAsync(id);

            if (staticData == null) return View("Error");

            return View(staticData);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteGet(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");

            StaticData? staticData = await _context.StaticDatas.FindAsync(id);

            if (staticData == null) return View("Error");
            ViewBag.Active = "Home";

            return View(staticData);
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
            StaticData? staticData = await _context.StaticDatas.FindAsync(id);

            staticData.IsActive = false;

            await _context.SaveChangesAsync();
            ViewBag.Active = "Home";

            return RedirectToAction(nameof(Generals));
        }
    }
}
