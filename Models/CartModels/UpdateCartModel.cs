namespace linhkien_donet.Models.CartModels
{
    public class UpdateCartModel
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }

        public int Quantity { get; set; }   
    }
}
