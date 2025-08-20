using Microsoft.AspNetCore.Identity;

namespace PharmacyManagement.Models
{
    public class Staff:IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;


        public ICollection<Order> Orders { get; set; }

    }
}
