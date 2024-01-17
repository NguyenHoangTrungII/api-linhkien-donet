using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Models.ProductModels;
using linhkien_donet.Models.UserModels;

namespace linhkien_donet.Interfaces
{
    public interface IUserRepository
    {
        Task<ApiResult<bool>> UpdateAvatar( UpdateAvatarRequest request);
    }
}
