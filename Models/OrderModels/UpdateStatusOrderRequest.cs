namespace linhkien_donet.Models.OrderModels
{
    public class UpdateStatusOrderRequest
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
    }
}
