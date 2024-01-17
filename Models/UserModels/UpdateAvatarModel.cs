using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Models.UserModels
{
    public class UpdateAvatarModel
    {
        [Required(ErrorMessage = "Not null")]
        public IFormFile Avatar { get; set; }
    }
}
