using Microsoft.AspNetCore.Identity;

namespace NET_CYBER_ASP_ENTITY.Models
{
    public class AppUser : IdentityUser
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
