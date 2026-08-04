using System.ComponentModel.DataAnnotations;

namespace Addressbook1.ViewModels.Account
{
    public record LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
