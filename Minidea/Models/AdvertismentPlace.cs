using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class AdvertismentPlace
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [StringLength(200, MinimumLength = 2)]
        public string? BigTitle { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<AdvertismentPhoto>? AdvertismentPhotos { get; set; }
        [NotMapped]
        public ICollection<IFormFile>? AllPhotos { get; set; }
    }
}
