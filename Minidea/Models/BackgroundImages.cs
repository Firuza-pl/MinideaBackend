using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class BackgroundImages
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? PhotoURL { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
