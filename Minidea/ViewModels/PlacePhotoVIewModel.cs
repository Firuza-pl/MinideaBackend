using Minidea.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.ViewModels
{
    public class PlacePhotoVIewModel
    {
        public int Id { get; set; }
        public string? BigTitle { get; set; }
        public string? AreaTitle { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<AdvertismentPhoto>? Photos { get; set; }
        [NotMapped]
        public ICollection<IFormFile>? AllPhotos { get; set; }
    }
}
