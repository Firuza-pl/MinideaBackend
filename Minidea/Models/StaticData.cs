using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class StaticData
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string? PhoneOne { get; set; }

        [StringLength(100)]
        public string? MobileTwo { get; set; }

        [ StringLength(100)]
        public string? MobileThree { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? FacebookLink { get; set; }

        [StringLength(100)]
        public string? InstagramLink { get; set; }

        [StringLength(100)]
        public string? LinkedinLink { get; set; }

        public string? PhotoURL { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }
        public bool IsActive { get; set; }
    }
}
