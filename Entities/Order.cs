using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace linhkien_donet.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; }
        public string Address { get; set; }

        public double Total { get; set; }

        public string Phone {  get; set; }

        [ForeignKey("UserId")]

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Payment Payment { get; set; }    

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }



    }
}
