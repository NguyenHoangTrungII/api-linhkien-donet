using _01_WEBAPI.Dto;
using linhkien_donet.Entities;

namespace linhkien_donet.Dto
{
    public class CartDetailDto
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public Int32 Quantity { get; set; }


    }
}
