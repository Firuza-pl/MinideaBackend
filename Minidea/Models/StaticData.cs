using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minidea.Models
{
    public class StaticData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [StringLength(200, MinimumLength = 2)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Etibarlı telefon nömrəsi deyil")]
        public string? PhoneOne { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [StringLength(200, MinimumLength = 2)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Etibarlı telefon nömrəsi deyil")]
        public string? MobileTwo { get; set; }

        [StringLength(100)]
        public string? MobileThree { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [StringLength(200, MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? FacebookLink { get; set; }

        [StringLength(100)]
        public string? InstagramLink { get; set; }

        [StringLength(100)]
        public string? LinkedinLink { get; set; }

        [Required(ErrorMessage = "Doldurulması vacibdir")]
        [NotMapped]
        public bool IsActive { get; set; }
    }
}
