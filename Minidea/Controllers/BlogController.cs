using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minidea.DAL;
using Minidea.Models;
using Minidea.ViewModels;

namespace Minidea.Controllers
{
    public class BlogController : Controller
    {
        private readonly Db_MinideaContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        public BlogController(Db_MinideaContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Blogs(int id)
        {

            IEnumerable<Blogs> blogs = null;

            if (id == 0)
            {
                blogs = _context.Blogs
                             .OrderByDescending(d => d.Date)
                                             .ToList();
            }
            else
            {
                IEnumerable<BlogsCategories> categories = _context.BlogsCategories.Where(d => d.Id == id).ToList();
                foreach (var item in categories)
                {
                    blogs = _context.Blogs.Where(d => d.CategoryId == item.Id)
                                 .OrderByDescending(d => d.Date)
                                                 .ToList();
                }
            }

            var list = new List<CategoryCountViewModel>();

            var query = _context.BlogsCategories
                    .Select(c => new CategoryCountViewModel
                    {
                        Id = c.Id,
                        Name = c.CategoryName,
                        Count = c.Blogss.Count()
                    })
                    .ToList();

            foreach (var item in query)
            {
                list.Add(new CategoryCountViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }


            BlogCategoryList blogCategoryList = new BlogCategoryList
            {
                Blogs = blogs,
                Categories = list,
                RecentBlogs = _context.Blogs.OrderByDescending(p => p.Date).Take(4).ToList()
            };
            return View(blogCategoryList);
        }


        public async Task<IActionResult> SingleBlog(int? id)
        {
            if (id == null) return View("Error");

            Blogs? news = await _context.Blogs.FindAsync(id);

            if (news == null) return View("Error");

            return View(news);
        }

    }
}
