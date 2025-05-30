using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
	public class BackgroundImages
	{
		public int Id { get; set; }
		public string? PhotoURL { get; set; } //file stored on server

		[NotMapped]
		public IFormFile? Photo { get; set; } //not saved to DB
		public bool IsActive { get; set; } = true; // you may want a default value
	}

}
