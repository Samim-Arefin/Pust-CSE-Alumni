using System.ComponentModel.DataAnnotations;

namespace Alumni.Domain.DtoModels
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Not a valid email"), MaxLength(128, ErrorMessage = "Max 128 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), Length(6, 32 , ErrorMessage = "Min 6 characters & Max 32 characters.")]
        public string Password { get; set; }
    }
}
