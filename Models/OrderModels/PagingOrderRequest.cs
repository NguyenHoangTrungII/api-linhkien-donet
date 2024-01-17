using linhkien_donet.Helper.ApiResults;

namespace linhkien_donet.Models.OrderModels
{
    public class PagingOrderRequest : PagingBase
    {
        public string? Status { get; set; }
    }
}
