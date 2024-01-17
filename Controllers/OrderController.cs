using linhkien_donet.Interfaces;
using linhkien_donet.Models.OrderModels;
using linhkien_donet.Models.PaymentModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace linhkien_donet.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class OrderController :Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IVnPayService vnPayService, IOrderRepository orderRepository = null)
        {
            _vnPayService = vnPayService;
            _orderRepository = orderRepository;
        }

        [HttpPost("create")]
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Json(url);
        }

        [HttpGet("payment-callback")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }

        [HttpPost("create-order")]
        //[Authorize(Roles = "Khách Hàng,Quản Trị Viên")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _orderRepository.CreateOrder(request);
            return Ok(result);
        }

        [HttpPost("get-order")]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<IActionResult> getOrderPaging([FromBody] PagingOrderRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _orderRepository.getOrderPaging(request, userId);
            return Ok(result);
        }
    }
}
