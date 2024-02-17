using _01_WEBAPI.Dto;
using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Dto;
using linhkien_donet.Entities;
using linhkien_donet.Models.CartModels;

namespace linhkien_donet.Interfaces
{
    public interface ICartRepository
    {
        Task<ApiResult<CartDto>> GetCart(string UserId);
        Task<ApiResult<CartDetailDto>> AddToCart(AddToCartModel request);

        Task<ApiResult<CartDetailDto>> UpdateCart(UpdateCartModel request);
        Task<ApiResult<bool>> RemoveItemFromCart(AddToCartModel request);

        Task<ApiResult<bool>> RemoveAllItem(string UserId);


    }
}
