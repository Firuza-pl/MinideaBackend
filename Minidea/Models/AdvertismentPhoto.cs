using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Minidea.Models
{
    public class AdvertismentPhoto
    {
        public int Id { get; set; }
        public int AdvertismentPlaceId { get; set; }
        public string? AreaTitle { get; set; }

        [Required, StringLength(100)]
        public string? PhotoURL { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }

        public bool IsMain { get; set; }

        public virtual AdvertismentPlace? AdvertismentPlace { get; set; }
    }
}
