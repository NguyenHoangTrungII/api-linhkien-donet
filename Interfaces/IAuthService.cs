using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Models.AuthModels;

namespace linhkien_donet.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResult<bool>> SeedRolesAsync();
        Task<ApiResult<bool>> RegisterAsync(RegisterModel registerModel);
        Task<ApiResult<LoginResponse>> LoginAsync(LoginModel loginModel);
        Task<ApiResult<bool>> MakeAdminAsync(UpdatePermission updatePermission);
        Task<ApiResult<bool>> MakeOwnerAsync(UpdatePermission updatePermission);

        Task<ApiResult<bool>> ForgotPassword(string email);

    }
}
