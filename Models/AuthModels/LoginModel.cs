using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Models.AuthModels
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
