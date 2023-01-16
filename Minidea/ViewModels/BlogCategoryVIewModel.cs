using Microsoft.AspNetCore.Mvc.ModelBinding;
using Minidea.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.ViewModels
{
    public class BlogCategoryVIewModel
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir"), StringLength(100)]
        public string? BigTitle { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir"), StringLength(100)]
        public string? SubTitle { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        public string? Text { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string? PhotoURL { get; set; }
        public BlogsCategories? Category { get; set; }
        public AppUser? User { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
