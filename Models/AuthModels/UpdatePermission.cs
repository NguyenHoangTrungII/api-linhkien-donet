using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Models.AuthModels
{
    public class UpdatePermission
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
    }
}
