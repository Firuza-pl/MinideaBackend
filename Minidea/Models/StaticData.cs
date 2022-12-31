using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class StaticData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi mütləq göstərilməlidir."), StringLength(100)]
        public string? PhoneOne { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi  mütləq göstərilməlidir."), StringLength(100)]
        public string? MobileTwo { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi  mütləq göstərilməlidir."), StringLength(100)]
        public string? MobileThree { get; set; }

        [Required(ErrorMessage = "Email ünvanı mütləq göstərilməlidir."), StringLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Facebook ünvanı mütləq göstərilməlidir."), StringLength(100)]
        public string? FacebookLink { get; set; }

        [Required(ErrorMessage = "İnstagram ünvanı mütləq göstərilməlidir."), StringLength(100)]
        public string? InstagramLink { get; set; }

        [Required(ErrorMessage = "Linkedin ünvanı mütləq göstərilməlidir."), StringLength(100)]
        public string? LinkedinLink { get; set; }

        [Required, StringLength(100)]
        public string? PhotoURL { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }
        public bool IsActive { get; set; }
    }
}
