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

            if (!ModelState.IsValid)
            {
                ViewBag.Active = "Site";
                ViewBag.Category = _context.BlogsCategories.ToList();

                return View(blogCategoryVIew);
            }

            if (blogCategoryVIew.CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Xahiş edirik kateqoriya seçin");
                return View(blogCategoryVIew);
            }

            if (blogCategoryVIew.Photo == null)
            {
                ModelState.AddModelError("Photo", "Xahiş edirik şəkil yükləyin.");
                return View(blogCategoryVIew);
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

        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            if (id == null) return View("Error");

            Blogs? blogs = await _context.Blogs.FindAsync(id);

            if (blogs == null) return View("Error");
            ViewBag.Active = "Home";

            return View(blogs);
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

            if (id == null) return View("Error");

            Blogs? data = await _context.Blogs.FindAsync(id);

            if (data == null) return View("Error");

            _context.Blogs.Remove(data);

            return View(data);
        }

        [ActionName("Edit")]
        public IActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");

            Blogs? data = _context.Blogs.Find(id);

            if (data == null) return View("Error");

            BlogCategoryVIewModel blogCategoryVIewModel = new BlogCategoryVIewModel
            {
                SubTitle = data.SubTitle,
                BigTitle = data.BigTitle,
                CategoryId = data.CategoryId,
                Text = data.Text,
                Date = data.Date,
                PhotoURL = data.PhotoUrl

            };
            ViewBag.Category = _context.BlogsCategories.ToList();

            return View(blogCategoryVIewModel);
        }

        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(BlogCategoryVIewModel blogCategoryVIew)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Blogs? newblogcategory = await _context.Blogs.FindAsync(blogCategoryVIew.Id);

            if (newblogcategory == null) return View("Error");


            if (blogCategoryVIew.Photo != null)
            {
                string computerPhoto = Path.Combine(_env.WebRootPath, "img", newblogcategory.PhotoUrl);

                if (System.IO.File.Exists(computerPhoto))
                {
                    System.IO.File.Delete(computerPhoto);
                }

                string filename = await blogCategoryVIew.Photo.SaveAsync(_env.WebRootPath, "blogs");
                blogCategoryVIew.PhotoURL = filename;
                newblogcategory.PhotoUrl = blogCategoryVIew.PhotoURL;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Blogs));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return View("Error");

            Blogs? data = await _context.Blogs.FindAsync(id);

            BlogsCategories? categories = await _context.BlogsCategories.FindAsync(data.CategoryId);

            if (data == null) return View("Error");

            BlogCategoryVIewModel blogCategoryVIewModel = new BlogCategoryVIewModel
            {
                SubTitle = data.SubTitle,
                BigTitle = data.BigTitle,
                CategoryName = categories.CategoryName,
                Text = data.Text,
                Date = data.Date,
                PhotoURL = data.PhotoUrl

            };
            ViewBag.Category = _context.BlogsCategories.ToList();

            return View(blogCategoryVIewModel);
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
        [ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(BlogsCategories blogsCategories)
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
