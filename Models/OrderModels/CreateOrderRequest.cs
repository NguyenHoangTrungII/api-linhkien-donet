using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Models.OrderModels
{
    public class CreateOrderRequest
    {
        

        [Required(ErrorMessage = "Not null")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Not Null")]
        public double Total { get; set; }
        [Required(ErrorMessage = "Not Null")]
        [StringLength(255, ErrorMessage = "Limit 255 words")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Not Null")]
        public string Phone { get; set; }
        //[Required(ErrorMessage = "Not Null")]
        //public string Note { get; set; }

        [Required(ErrorMessage = "Not Null")]
        public List<CreateOrderDetailRequest> Details { get; set; }
    }
}
