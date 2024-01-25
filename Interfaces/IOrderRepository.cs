using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Entities;
using linhkien_donet.Helper.ApiResults;
using linhkien_donet.Models.OrderModels;
using linhkien_donet.Models.PaymentModels;
using Microsoft.AspNetCore.Http;

namespace linhkien_donet.Interfaces
{
    public interface IOrderRepository
    {
        //Task<ApiResult<int>> CreatePayment(CreateOrderRequest request);

        //Task<ApiResult<int>> CreateOrder(CreateOrderRequest request);

        Task<ApiResult<bool>> InitiatePaymentAndGetRedirectUrl(PaymentInformationModel paymentInfo, HttpContext context);

        Task<ApiResult<bool>> ProcessVNPayReturn(PaymentResponseModel response);

        Task<ApiResult<bool>> CreateOrder(string userId, CreateOrderRequest response);

        Task<ApiResult<bool>> UpdateStatusOrder(UpdateStatusOrderRequest request );


        Task<PagingApi<List<Order>>> getOrderPaging(PagingOrderRequest request, string userId);


    }
}
