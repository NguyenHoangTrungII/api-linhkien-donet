using _01_WEBAPI.Dto;
using _01_WEBAPI.Interfaces;
//using _01_WEBAPI.Respository;
using AutoMapper;
using linhkien_donet.Entities;
using linhkien_donet.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _01_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryReposity;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryReposity = categoryRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetAllCategory()
        {
            var brands = _categoryReposity.GetAllCategory();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(brands);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]

        public IActionResult GetCategoryById(int categoryId)
        {
            if (!_categoryReposity.CategoryExists(categoryId))
                return NotFound();

            //var brand = _brandRepository.GetBrandById(categoryId);
            var category = _mapper.Map<CategoryDto>(_categoryReposity.GetCategoryById(categoryId));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = (" OWER"))]

        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto CategoryCreate)
        {
            if (CategoryCreate == null)
                return BadRequest(ModelState);

            var country = _categoryReposity.GetAllCategory()
                .Where(c => c.Name.Trim().ToUpper() == CategoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cat = _mapper.Map<Category>(CategoryCreate);

            if (_categoryReposity.CreateCategory(cat) == 0)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok(cat);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = (" OWER"))]

        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updateCategory)
        {
            if (updateCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updateCategory.Id)
                return BadRequest(ModelState);

            if (!_categoryReposity.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Category>(updateCategory);

            if (_categoryReposity.UpdateCategory(categoryMap) == 0)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = (" OWER"))]

        public async Task<IActionResult> DeleteCategory(int categoryId)
        {

            if (!_categoryReposity.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var countryToDelete = _categoryReposity.GetCategoryById(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_categoryReposity.DeleteCategory(countryToDelete) == 0)
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}
