using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Models.PaymentModels;

namespace linhkien_donet.Interfaces
{
    public interface IPaymentRepository
    {

        Task<ApiResult<bool>> CreatePayment(PaymentResponseModel request);

        //Task<ApiResult<bool>> CreatePaymentUrl(PaymentInformationModel request, HttpContext context);


    }
}
