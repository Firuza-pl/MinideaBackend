using Minidea.Models;

namespace Minidea.ViewModels
{
    public class BlogAdsViewModel
    {
        public virtual IEnumerable<AdvertismentPlace>? Places { get; set; }
        public virtual IEnumerable<Blogs>? Blogs { get; set; }

    }
}
