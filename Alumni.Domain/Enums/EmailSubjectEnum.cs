using System.ComponentModel.DataAnnotations;

namespace Alumni.Domain.Enums
{
    public enum EmailSubjectEnum
    {
        [Display(Name = "Email Confirmation")]
        EmailConfirmation,
        [Display(Name = "Reset Password Email")]
        ResetPasswordEmail
    }
}
