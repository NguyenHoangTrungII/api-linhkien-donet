using linhkien_donet.Helper.ApiResults;

namespace linhkien_donet.Models.ProductModels
{
    public class GetProductPagingModel : PagingBase
    {
        //public string? keyword { get; set; }
        public int? categoryId { get; set; }
        public int? brandId { get; set; }
    }
}
