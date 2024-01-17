using _01_WEBAPI.Data;
using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Entities;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.PaymentModels;

namespace linhkien_donet.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext _context;
        

        public PaymentRepository(DataContext context)
        {
            _context = context;
            
        }

        public async Task<ApiResult<bool>> CreatePayment(PaymentResponseModel request)
        {
            try
            {
                if(request.PaymentMethod != null && request.OrderId != null)
                {
                    var payment = new Payment()
                    {
                        PaymentId = request.PaymentId,
                        TransactionId = request.TransactionId,
                        Content = request.OrderDescription,
                        PaymentMethod = request.PaymentMethod,
                        PaymentStatus = request.Success,
                        Token = request.Token,
                        OrderId = Int32.Parse(request.OrderId),
                    };
                    await _context.Payment.AddAsync(payment);
                    var result = await _context.SaveChangesAsync() > 0;
                    return new ApiSuccessResult<bool>(result);

                }

                return new ApiSuccessResult<bool>("PaymentMethod and OrderId not null");
            }
            catch (Exception ex)
            {
                return new ApiFailResult<bool>(ex.Message);
            }
        }

    }
}
