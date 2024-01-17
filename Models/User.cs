using linhkien_donet.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_WEBAPI.Models

{
    public class User 
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string Avatar { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Account> Accounts { get; set; }

        


    }
}
