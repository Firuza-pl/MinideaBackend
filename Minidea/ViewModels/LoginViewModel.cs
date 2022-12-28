using System.ComponentModel.DataAnnotations;

namespace Minidea.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email boş olmamalıdır"), EmailAddress(ErrorMessage = "Düzgün email daixl edin"), DataType(DataType.EmailAddress, ErrorMessage = "Düzgün email daxil edin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parol boş olmamalıdır"), DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
