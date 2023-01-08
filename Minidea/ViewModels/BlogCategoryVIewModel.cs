using Minidea.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.ViewModels
{
    public class BlogCategoryVIewModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? UserId { get; set; }
        public string? CategoryName { get; set; }
        public string? BigTitle { get; set; }
        public string? SubTitle { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        public string PhotoURL { get; set; }
        public BlogsCategories? Category { get; set; }
        public AppUser? User { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
