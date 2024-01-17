using _01_WEBAPI.Models;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.ProductModels;
using linhkien_donet.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace linhkien_donet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProdudctRepository _produdctRepository;

        public ProductController(IProdudctRepository produdctRepository)
        {
            _produdctRepository = produdctRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {

            var result = await _produdctRepository.GetAllProduct();

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("byid/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {

            var result = await _produdctRepository.GetProductById(productId);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("byname/{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductByName([FromRoute] string name)
        {

            var result = await _produdctRepository.GetProductByName(name);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("public/advanced-search")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicProductPaging([FromQuery] GetProductPagingModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _produdctRepository.GetPublicProductPaging(request);
            return Ok(result);
        }

        [HttpPost("images")]
        //[Authorize(Roles = "ADMIN")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProductImage([FromForm] CreateImagesModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _produdctRepository.CreateImages(request);
            return Ok(result);
        }

        [HttpPost("imagesbycloudinary")]
        //[Authorize(Roles = "ADMIN")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProductImageByCloundinary([FromForm] CreateImagesModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _produdctRepository.CreateImagesByCloudinary(request);
            return Ok(result);
        }

        [HttpDelete("image/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _produdctRepository.DeleteProductImage(id);
            return Ok(result);
        }

        [HttpPost("product")]
        //[Authorize(Roles = "ADMIN")]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _produdctRepository.CreateProduct(request);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _produdctRepository.DeleteProduct(productId);
            return Ok(result);
        }



        

    }
}
