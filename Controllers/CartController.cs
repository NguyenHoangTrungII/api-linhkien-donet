using _01_WEBAPI.Models;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.CartModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using _01_WEBAPI.Dto;
using _01_WEBAPI.Interfaces;
using linhkien_donet.Entities;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace linhkien_donet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("me")]
        [Authorize(Roles =("USER"))]
        public async Task<IActionResult> GetCart()
        {
            var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _cartRepository.GetCart(userId);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addToCart/{productId}")]
        [Authorize(Roles = ("USER"))]

        public async Task<IActionResult> AddToCart([FromRoute] int productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var request = new AddToCartModel
            {
                ProductId = productId,
                UserId = userId
            };

            var result = await _cartRepository.AddToCart(request);



            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);

            
        }

        [HttpPatch("items/{productId}")]
        [Authorize(Roles = ("USER, OWER"))]

        public async Task<IActionResult> UpdateCart([FromRoute] int ProductId, [FromBody] UpdateProductCartRequest data)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //int Quantity = data["quantity"].ToObject<int>();

          

            var request = new UpdateCartModel
            {
                ProductId = ProductId,
                UserId = userId,
                Quantity = data.Quantity
            };

            var result = await _cartRepository.UpdateCart(request);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("items/{productId}")]
        [Authorize(Roles ="USER, ADMIN")]
        public async Task<IActionResult> RemoveItemFromCart([FromRoute] int ProductId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var request = new AddToCartModel
            {
                ProductId = ProductId,
                UserId = userId,
            };

            var result = await _cartRepository.RemoveItemFromCart(request);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("items")]
        [Authorize(Roles = "USER, ADMIN")]
        public async Task<IActionResult> RemoveAllItems()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            

            var result = await _cartRepository.RemoveAllItem(userId);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
