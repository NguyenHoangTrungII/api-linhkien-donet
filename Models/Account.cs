using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_WEBAPI.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public  String Password { get; set; }
        public string Phone { get; set; }
        public List<string> Role { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public Models.User User { get; set; }
    }
}
