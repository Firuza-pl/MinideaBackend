using Minidea.Models;

namespace Minidea.ViewModels
{
    public class BlogCategoryList
    {
        public virtual IEnumerable<Blogs>? Blogs { get; set; }
        public virtual IEnumerable<CategoryCountViewModel>? Categories { get; set; }
        public int Count { get; set; }
        public virtual IEnumerable<Blogs>? RecentBlogs { get; set; }

    }
}
