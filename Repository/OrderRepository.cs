using _01_WEBAPI.Data;
using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Entities;
using linhkien_donet.Helper.ApiResults;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.OrderModels;
using linhkien_donet.Models.PaymentModels;
using linhkien_donet.Services;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;

namespace linhkien_donet.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IVnPayService _vnPayService;
        private readonly IPaymentRepository _paymentRepository;

        public OrderRepository(IVnPayService vnPayService, IPaymentRepository paymentRepository, DataContext context)
        {
            _vnPayService = vnPayService;
            _paymentRepository = paymentRepository;
            _context = context;
        }

        public async Task<ApiResult<bool>> InitiatePaymentAndGetRedirectUrl(PaymentInformationModel paymentInfo, HttpContext context)
        {
            if(context == null)
            {
                return new ApiFailResult<bool>("Httpcontext don't null");
            }
            
            var paymentUrl =  _vnPayService.CreatePaymentUrl(paymentInfo, context);
            //        _paymentRepository.CreatePaymentInfo(response);


            return new ApiSuccessResult<bool>(paymentUrl);
        }

        public async Task<ApiResult<bool>> ProcessVNPayReturn(PaymentResponseModel response)
        {
            _paymentRepository.CreatePayment(response);

            if (response.Success)
            {
                return new ApiSuccessResult<bool>("Payment success");
            }
            else
            {
                return new ApiFailResult<bool>("Payment failed");
            }
        }

        public async Task<ApiResult<bool>> CreateOrder(string userId, CreateOrderRequest request)
        {
            var listDetailOrder = new List<OrderDetail>();

            foreach (var item in request.Details)
            {
                OrderDetail detail = new OrderDetail();
                detail.Price = item.Price;
                detail.Quantity = item.Quantity;
                detail.ProductId = item.ProductId;

                listDetailOrder.Add(detail);
            }

            var order = new Order()
            {
                UserId = userId,
                Total = request.Total,
                Address = request.Address,
                Phone = request.Phone,
                Status = request.Status,
                CreatedDate = DateTime.Now,
                //Note = request.Note,
                OrderDetails = listDetailOrder
            };

            _context.Order.AddAsync(order);

            
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> UpdateStatusOrder(UpdateStatusOrderRequest request)
        {
            try
            {
                var order = await _context.Order.FirstOrDefaultAsync(o=>o.Id == request.OrderId);

                if (order == null)
                {
                    return new ApiFailResult<bool>("Order is not found");
                };

                order.Status = request.Status;


                var result = await _context.SaveChangesAsync() > 0;


                return new ApiSuccessResult<bool>(result);
            }
            catch (Exception ex)
            {
                return new ApiFailResult<bool>(ex.Message);
            }
        }


        public async Task<PagingApi<List<Order>>> getOrderPaging(PagingOrderRequest request, string userId)
        {

            var query = from o in _context.Order  
                        orderby o.CreatedDate descending
                        select new { o };

            var listOrderDetails = from od in _context.OrderDetail
                                   join p in _context.Product on od.ProductId equals p.Id
                                   select new { p, od };


            if(userId != null)
            {
                query = query.Where(x => x.o.UserId == userId);

            }

            if (request.Status != null)
            {

                query = query.Where(x => x.o.Status == request.Status);
            }

            int totalRecordAll = await query.CountAsync();

            if (query.Count() > 0)
            {
                query = query.Skip(((request.PageIndex - 1) * request.PageSize)).Take(request.PageSize).OrderByDescending(x => x.o.CreatedDate);
            }

            var listOrders = await query.Select(x => new Order()
            {
                Id = x.o.Id,
                Total = x.o.Total,
                Address = x.o.Address,
                Phone = x.o.Phone,
                Status = x.o.Status.ToString(),
                CreatedDate = x.o.CreatedDate,
                //Note = x.o.Note,
                OrderDetails = listOrderDetails.Where(a => a.od.OrderId == x.o.Id).Select(q => new OrderDetail()
                {
                    OrderId = x.o.Id,
                    ProductId = q.od.ProductId,
                    Product = q.p,
                    Price = q.od.Price,
                    Quantity = q.od.Quantity,
                    
                }).ToList()
            }).ToListAsync();


            int totalRecord = await query.CountAsync();
            double totalPage = Math.Ceiling((double)totalRecordAll / request.PageSize);

            return new PagingSuccessResult<List<Order>>(totalRecordAll, totalPage, totalRecord, request.PageSize, request.PageIndex, listOrders);
        }

       

    }
}
