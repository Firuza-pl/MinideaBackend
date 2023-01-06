using Minidea.Models;

namespace Minidea.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<AdvertismentPlace>? advertismentPlaces { get; set; }
        public IEnumerable<AdvertismentPhoto>? advertismentPhotos { get; set; }

    }
}
