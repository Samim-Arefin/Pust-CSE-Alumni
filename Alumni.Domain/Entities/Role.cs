using Microsoft.AspNetCore.Identity;

namespace Alumni.Domain.Entities
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
