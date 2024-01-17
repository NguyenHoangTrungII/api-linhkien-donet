using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Models.ProductModels
{
    public class CreateImagesModel
    {
        [Required(ErrorMessage = "Not null")]
        public int ProductId { get; set; }

        
        [Required(ErrorMessage = "Not null")]
        public List<IFormFile> Image { get; set; }
    }
}
