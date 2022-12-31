using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class Blogs
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } 
        public virtual BlogsCategories? Category { get;set;}
        public virtual AppUser?  User { get; set; }
        public string? BigTitle { get; set; }
        public string? SubTitle { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }

        [Required,StringLength(100)]
        public string? PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
