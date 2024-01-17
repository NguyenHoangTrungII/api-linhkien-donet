using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
