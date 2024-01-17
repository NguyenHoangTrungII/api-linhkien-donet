using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Models.UserModels
{
    public class UpdateAvatarRequest
    {
        [Required(ErrorMessage = "Not null")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Not null")]
        public IFormFile Avatar { get; set; }
    }
}
