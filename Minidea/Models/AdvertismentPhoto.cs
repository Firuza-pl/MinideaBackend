using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Minidea.Models
{
    public class AdvertismentPhoto
    {
        public int Id { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Doldurulması vacibdir")]
        public int AdvertismentPlaceId { get; set; }

        [Required]
        public string? AreaTitle { get; set; }

        [Required, StringLength(100)]
        public string? PhotoURL { get; set; }

        [Required]
        [NotMapped]
        public IFormFile? Photo { get; set; }
        public bool IsMain { get; set; }

        [Required]
        [NotMapped]
        public virtual AdvertismentPlace? AdvertismentPlace { get; set; }


    }
}
