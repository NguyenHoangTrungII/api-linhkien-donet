using linhkien_donet.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Models.OrderModels
{
    public class CreateOrderDetailRequest
    {
        public short Quantity { get; set; }

        public double Price { get; set; }

        public int ProductId { get; set; }

    }
}
