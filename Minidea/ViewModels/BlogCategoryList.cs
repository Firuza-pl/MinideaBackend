using Minidea.Models;

namespace Minidea.ViewModels
{
    public class BlogCategoryList
    {
        public virtual IEnumerable<Blogs>? Blogs { get; set; }
        public virtual IEnumerable<BlogsCategories>? Categories { get; set; }
        public virtual IEnumerable<Blogs>? RecentBlogs { get; set; }

    }
}
