using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace linhkien_donet.Entities
{
    public class CartDetail
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }

        //[ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        //[ForeignKey("CartId")] 
        public int CartId { get; set; }
        public Cart Cart { get; set; }

    }
}
