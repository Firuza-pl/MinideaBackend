using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class BackgroundImages
    {
        public int Id { get; set; }
        public string? PhotoURL { get; set; }
        [NotMapped]
        public IFormFile? Photo{ get; set; }
        public bool IsActive { get; set; }

    }
}
