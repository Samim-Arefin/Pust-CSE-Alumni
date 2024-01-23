using System.ComponentModel.DataAnnotations;

namespace Alumni.Domain.DtoModels
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), Length(6, 32, ErrorMessage = "Min 6 characters & Max 32 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^(09|1[0-9]|2[0-3])01(0[1-9]|[1-9][0-9])$", ErrorMessage = "Enter valid Roll")]
        public string Roll { get; set; }

    }
}
