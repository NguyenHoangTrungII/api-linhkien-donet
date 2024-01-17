using _01_WEBAPI.Dto;
using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Entities;
using linhkien_donet.Helper.ApiResults;
using linhkien_donet.Models.ProductModels;
using System;

namespace linhkien_donet.Interfaces
{
    public interface IProdudctRepository
    {
        Task<ApiResult<List<Product>>> GetAllProduct();
        Task<ApiResult<ProductDto>> GetProductById(int id);
        Task<ApiResult<List<ProductDto>>> GetProductByName(string name);

        Task<PagingApi<List<ProductDto>>> GetPublicProductPaging(GetProductPagingModel request);


        Task<ApiResult<bool>> CreateImages(CreateImagesModel request);
        Task<ApiResult<bool>> CreateImagesByCloudinary(CreateImagesModel request);

        Task<ApiResult<bool>> DeleteProductImage(int imageId);

        Task<ApiResult<bool>> CreateProduct(CreateProductModel request);

        Task<ApiResult<bool>> DeleteProduct(int ProductId);


    }
}
