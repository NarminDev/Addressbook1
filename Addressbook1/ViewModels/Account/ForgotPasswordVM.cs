using System.ComponentModel.DataAnnotations;

namespace Addressbook1.ViewModels.Account
{
    public record ForgotPasswordVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
    }
}
