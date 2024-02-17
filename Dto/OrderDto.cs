using linhkien_donet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace linhkien_donet.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string Status { get; set; }
        public string Address { get; set; }

        public double Total { get; set; }

        public string Phone { get; set; }

        public string UserId { get; set; }

        public ICollection<OrderDetailDto> OrderDetails { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
