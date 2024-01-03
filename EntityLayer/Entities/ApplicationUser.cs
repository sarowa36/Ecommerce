using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
