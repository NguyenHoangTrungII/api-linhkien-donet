using linhkien_donet.Entities;

namespace linhkien_donet.Dto
{
    public class CartDto
    {
        public int Id { get; set; }
        public List<CartDetailDto> Items { get; set; }

        public string UserId { get; set; }

    }
}
