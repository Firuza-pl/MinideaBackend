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
using System.Security.Claims;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly Db_MinideaContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public BlogController(Db_MinideaContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> siginInManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _siginInManager = siginInManager;
            _env = env;
        }

        public IActionResult Blogs()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(_context.Blogs.ToList());
        }

        public IActionResult Create(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Category = _context.BlogsCategories.ToList();
            ViewBag.Active = "Home";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategoryVIewModel blogCategoryVIew)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
          
            if (!blogCategoryVIew.Photo.IsImage())
            {
                ViewBag.Active = "Home";

                ModelState.AddModelError("Photo", "File type should be image");

                return View(blogCategoryVIew);
            }

            string filename = await blogCategoryVIew.Photo.SaveAsync(_env.WebRootPath, "blogs");
            blogCategoryVIew.PhotoURL = filename;


            Blogs blogs = new Blogs
            {
                PhotoUrl = filename,
                CategoryId = blogCategoryVIew.CategoryId,
                BigTitle = blogCategoryVIew.BigTitle,
                SubTitle = blogCategoryVIew.SubTitle,
                Text = blogCategoryVIew.Text,
                Date = DateTime.Now,
            };

            await _context.Blogs.AddAsync(blogs);

            await _context.SaveChangesAsync();

            return RedirectToAction("Blogs");
        }

        //Category

        public IActionResult BlogCategory()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_context.BlogsCategories.ToList());
        }

        public IActionResult CreateCategory(int? id)
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
        public async Task<IActionResult> CreateCategory(BlogsCategories blogsCategories)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Active = "Home";

                return View(blogsCategories);
            }
            ViewBag.Active = "Home";
            BlogsCategories categories = new BlogsCategories
            {
                CategoryName = blogsCategories.CategoryName
            };

            await _context.BlogsCategories.AddAsync(categories);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BlogCategory));
        }

        [ActionName("EditCategory")]
        public IActionResult EditCategory(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            BlogsCategories? data = _context.BlogsCategories.Find(id);

            ViewBag.Active = "Home";
            return View(data);
        }

        [ActionName("EditCategory")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(BlogsCategories blogsCategories)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            BlogsCategories? categories = await _context.BlogsCategories.FindAsync(blogsCategories.Id);

            if (categories == null) return View("Error");

            categories.CategoryName = blogsCategories.CategoryName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(BlogCategory));
        }

        [ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");

            BlogsCategories? data = await _context.BlogsCategories.FindAsync(id);


            if (data == null) return View("Error");
            ViewBag.Active = "Home";

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteSinglePlace")]
        public async Task<IActionResult> DeleteSinglePlace(BlogsCategories blogsCategories)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            BlogsCategories? categories = await _context.BlogsCategories.FindAsync(blogsCategories.Id);

            IEnumerable<Blogs> blogs = _context.Blogs.Where(p => p.CategoryId == categories.Id).ToList();

            _context.Blogs.RemoveRange(blogs);
            _context.BlogsCategories.Remove(categories);

            await _context.SaveChangesAsync();
            ViewBag.Active = "Home";

            return RedirectToAction(nameof(BlogsCategories));
        }
    }
}
