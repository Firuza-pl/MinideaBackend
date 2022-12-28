using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class AdvertismentPlace
    {
        public int Id { get; set; }
        public string? BigTitle { get; set; }

        public virtual ICollection<AdvertismentPhoto>? AdvertismentPhotos { get; set; }
        [NotMapped]
        public ICollection<IFormFile>? AllPhotos { get; set; }
    }
}
