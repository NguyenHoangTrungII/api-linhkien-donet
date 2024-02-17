using _01_WEBAPI.Dto;
using linhkien_donet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace linhkien_donet.Dto
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public short Quantity { get; set; }

        public double Price { get; set; }

        public ProductDto Product { get; set; }

        public int OrderId { get; set; }
    }
}
