using _01_WEBAPI.Data;
using _01_WEBAPI.Dto;
using _01_WEBAPI.Interfaces;
using AutoMapper;
using linhkien_donet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace _01_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Brand>))]
        public async Task<IActionResult> GetAllBrand()
        {

            var brands = _mapper.Map<List<BrandDto>>(await _brandRepository.GetAllBrand());


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(brands);
        }


        [HttpGet("{brandId}")]
        [ProducesResponseType(200, Type = typeof(Brand))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBrandById(int BrandId)
        {
            if (!await _brandRepository.BrandExists(BrandId))
                return NotFound();

            //var brand = _brandRepository.GetBrandById(BrandId);
            var brand = _mapper.Map<BrandDto>(await _brandRepository.GetBrandById(BrandId));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(brand);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> CreateBrand([FromBody] BrandDto BrandCreate)
        {
            if (BrandCreate == null)
                return BadRequest(ModelState);

            var existingBrands = await _brandRepository.GetAllBrand();
            var existingBrand = existingBrands.FirstOrDefault(c => c.Name.Trim().ToUpper() == BrandCreate.Name.Trim().ToUpper());


            if (existingBrand != null)
            {
                ModelState.AddModelError("", "Brand Name already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Brand>(BrandCreate);

            if (await _brandRepository.CreateBrand(countryMap) == false)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok(countryMap);
        }

        [HttpPut("{brandId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> updateBrand(int brandId, [FromBody] BrandDto updateBrand)
        {
            if (updateBrand == null)
                return BadRequest(ModelState);



            if (!await _brandRepository.BrandExists(brandId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var brandMap = _mapper.Map<Brand>(updateBrand);

            if (!await _brandRepository.UpdateBrand(brandMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{brandId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> DeleteBrand(int brandId)
        {

            if (!await _brandRepository.BrandExists(brandId))
            {
                return NotFound();
            }

            var brandeleted = await _brandRepository.GetBrandById(brandId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _brandRepository.DeleteBrand(brandeleted))
            {
                ModelState.AddModelError("", "Something went wrong deleting brand");
            }

            return NoContent();
        }


    }
}
