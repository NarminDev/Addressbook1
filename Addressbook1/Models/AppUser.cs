using Microsoft.AspNetCore.Identity;

namespace Addressbook1.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
