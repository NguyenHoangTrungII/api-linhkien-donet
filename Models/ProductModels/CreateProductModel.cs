using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Models.ProductModels
{
    public class CreateProductModel
    {
        [Required(ErrorMessage = "Not null")]
        [StringLength(255, ErrorMessage = "Cannot exceed 255 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Not null")]
        public Double OldPrice { get; set; }

        [Required(ErrorMessage = "Not null")]
        public Double Price { get; set; }

        [Required(ErrorMessage = "Not null")]
        public Int32 Quantity { get; set; }

        [Required(ErrorMessage = "Not null")]
        [StringLength(255, ErrorMessage = "Cannot exceed 255 characters")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Not null")]
        public Int32 StatusProduct { get; set; }

        [Required(ErrorMessage = "Not null")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Not null")]
        public int BrandId { get; set; }


        
    }
}
