using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace linhkien_donet.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Avatar {  get; set; }

        public Cart Cart { get; set; }

        public List<Order> Orders { get; set; }
    }
}
