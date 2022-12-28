using System.ComponentModel.DataAnnotations;

namespace Minidea.Models
{
    public class BlogsCategories
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kateqoriyanın adı mütləq doldurulmalıdır."), StringLength(100, ErrorMessage = "Kateqoriyanın adı 100 simvoldan çox ola bilməz")]
        public string? CategoryName { get; set; }
        public int? Type { get; set; }
        public int? Count { get; set; }
        public virtual ICollection<BlogsCategories>? Categories { get; set; }
    }
}
