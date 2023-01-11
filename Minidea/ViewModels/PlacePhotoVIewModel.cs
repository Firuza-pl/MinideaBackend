using Minidea.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.ViewModels
{
    public class PlacePhotoVIewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        public int AdvertismentPlaceId { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [StringLength(200, MinimumLength = 2)]
        public string AreaTitle { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<AdvertismentPhoto>? Photos { get; set; }

        [Required]
        public AdvertismentPlace? advertismentPlace { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [NotMapped]
        public ICollection<IFormFile>? AllPhotos { get; set; }
    }
}
