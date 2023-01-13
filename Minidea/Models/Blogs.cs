using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class Blogs
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } 
        public virtual BlogsCategories? Category { get;set;}

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        public string? BigTitle { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        public string? SubTitle { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        public string? Text { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required,StringLength(100)]
        public string? PhotoUrl { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
