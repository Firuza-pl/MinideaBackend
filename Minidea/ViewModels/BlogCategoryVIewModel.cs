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

		[Required(ErrorMessage = "It is important to fill it out"), StringLength(100)]
		public string BigTitle { get; set; } = null!;

		[Required(ErrorMessage = "It is important to fill it out."), StringLength(100)]
		public string SubTitle { get; set; } = null!;

		[Required(ErrorMessage = "It is important to fill it out.")]
		public string Text { get; set; } = null!;

		[DataType(DataType.Date)]
		public DateTime Date { get; set; } 

		public string? PhotoURL { get; set; }

		public BlogsCategories? Category { get; set; }

		public AppUser? User { get; set; }

		[Required(ErrorMessage = "It is important to fill it out.")]
		[NotMapped]
		public IFormFile? Photo { get; set; }
	}

}
