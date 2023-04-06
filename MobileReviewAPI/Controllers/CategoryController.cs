using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Interfaces;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<Category>> GetAllCategories()
        {
            var category = await _categoryRepository.GetCategories();
            var mapCategory = _mapper.Map<IEnumerable<CategoryDto>>(category);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapCategory);

        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Category>> GetCategoryById(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var category = await _categoryRepository.GetCategoryById(categoryId);
            var mapCategory = _mapper.Map<CategoryDto>(category);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapCategory);
        }

        [HttpGet("Mobile/{categoryId}")]
        public async Task<ActionResult<Mobile>> GetMobilesByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var mobile = await _categoryRepository.GetMobileByCategory(categoryId);
            var mobileDto = _mapper.Map<IEnumerable<MobileDto>>(mobile);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mobileDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto createCategory)
        {
            if (createCategory == null)
            {
                return BadRequest(ModelState);
            }
            var categories = await _categoryRepository.GetCategories();

            var existingCategory = categories.FirstOrDefault(c => c.Name.Trim().ToUpper() == createCategory.Name.TrimEnd().ToUpper());

            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Category already exists.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryMap = _mapper.Map<Category>(createCategory);
            if (!await _categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest(ModelState);
            }

            if (categoryId != updatedCategory.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if (!await _categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId}")]

        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var categoryDelete = await _categoryRepository.GetCategoryById(categoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _categoryRepository.DeleteCategory(categoryDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }
            return NoContent();

        }

    }
}