using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace linhkien_donet.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double OldPrice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int StatusProduct { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        [ForeignKey("BrandId")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public List<CartDetail> CartDetail { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public List<Images> Images { get; set; }



    }
}
