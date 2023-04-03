using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Models;
using MobileReviewAPI.Repositories;

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

    }
}