using Microsoft.AspNetCore.Identity;

namespace Domain_Layer.Models.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;

        public Address? Address { get; set; }
    }
}
