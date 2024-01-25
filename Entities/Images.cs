using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace linhkien_donet.Entities
{
    public class Images
    {
        public int Id { get; set; }
        public string images { get; set; }

        public bool IsThumbail { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
