using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace linhkien_donet.Entities
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public short Quantity { get; set; }

        public double Price { get; set; }

        // Khóa ngoại đến Product
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Khóa ngoại đến Order
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
