using linhkien_donet.Interfaces;
using linhkien_donet.Models.OrderModels;
using linhkien_donet.Models.PaymentModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace linhkien_donet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController :Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IVnPayService vnPayService, IOrderRepository orderRepository = null)
        {
            _vnPayService = vnPayService;
            _orderRepository = orderRepository;
        }

        [HttpPost("paymentURL")]
        [Authorize(Roles ="USER")]
        public IActionResult CreatePaymentUrl([FromBody] PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Json(url);
        }

        [HttpGet("paymentCallback")]
        [Authorize(Roles = "USER")]

        public IActionResult PaymentCallback( )
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }

        [HttpPost("createOrder")]
        [Authorize(Roles = "USER, ADMIN")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _orderRepository.CreateOrder(userId, request);
            return Ok(request);
        }

        [HttpPost("getOrder")]
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

        [HttpPost("changeStatus")]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<IActionResult> UpdateStatusOrder([FromBody] UpdateStatusOrderRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _orderRepository.UpdateStatusOrder(request);
            return Ok(result);
        }
    }
}
