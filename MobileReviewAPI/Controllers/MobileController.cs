using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Interfaces;
using MobileReviewAPI.Models;
using MobileReviewAPI.Repositories;
using System.Linq;


namespace MobileReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly IMobileRepository _mobileRepository;
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;

        public MobileController(IMobileRepository mobileRepository, IMapper mapper, IOwnerRepository ownerRepository, ICategoryRepository categoryRepository, IReviewRepository reviewRepository)
        {
            _mobileRepository = mobileRepository;
            _mapper = mapper;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Mobile>> GetAllMobileAsync()
        {
            var mobiles = await _mobileRepository.GetAllMobileAsync();
            var mobileDtos = _mapper.Map<IEnumerable<MobileDto>>(mobiles);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mobileDtos);
        }

        [HttpGet("{mobId}")]
        public async Task<ActionResult<Mobile>> GetMobileId(int mobId)
        {
            if (!_mobileRepository.MobileExists(mobId))
            {
                return NotFound();
            }

            var mobile = await _mobileRepository.GetMobileIdAsync(mobId);
            var mobileDto = _mapper.Map<MobileDto>(mobile);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mobileDto);
        }

        [HttpGet("Name/{mobName}")]

        public async Task<ActionResult<Mobile>> GetMobileByName(string mobName)
        {

            var mobiles = await _mobileRepository.GetMobileNamesAsync(mobName);
            var mobileDtos = _mapper.Map<IEnumerable<MobileDto>>(mobiles);

            if (mobileDtos == null)
            {
                return NotFound();
            }
            return Ok(mobileDtos);
        }

        [HttpGet("Rating/{mobId}")]
        public async Task<ActionResult<decimal>> GetMobileRating(int mobId)
        {
            if (!_mobileRepository.MobileExists(mobId))
            {
                return NotFound();
            }
            var rating = await _mobileRepository.GetMobileRating(mobId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rating);
        }



        [HttpPost]
        public async Task<ActionResult> CreateMobile([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] MobileDto createMobile)
        {
            if (createMobile == null)
            {
                return BadRequest(ModelState);
            }

            var mobiles = await _mobileRepository.GetAllMobileAsync();
            var mobileExists = mobiles.FirstOrDefault(m => m.Name.Trim().ToUpper() == createMobile.Name.TrimEnd().ToUpper());
            if (mobileExists != null)
            {
                ModelState.AddModelError("", "Mobile already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mobileMap = _mapper.Map<Mobile>(createMobile);


            if (!await _mobileRepository.CreateMobile(ownerId, catId, mobileMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }


        [HttpPut("{mobileId}")]
        public async Task<IActionResult> UpdateMobile(int mobileId, [FromQuery] int ownerId, [FromQuery] int catId, [FromBody] MobileDto updatedMobile)
        {
            if (updatedMobile == null)
            {
                return BadRequest(ModelState);
            }

            if (mobileId != updatedMobile.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_mobileRepository.MobileExists(mobileId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mobileMap = _mapper.Map<Mobile>(updatedMobile);

            if (!await _mobileRepository.UpdateMobile(ownerId, catId, mobileMap))
            {
                ModelState.AddModelError("", "Something went wrong updating mobile");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{mobileId}")]
        public async Task<IActionResult> DeleteMobile(int mobileId)
        {
            if (!_mobileRepository.MobileExists(mobileId))
            {
                return NotFound();
            }

            var reviewsToDelete = await _reviewRepository.GetMobileReviews(mobileId);
            var mobileToDelete = await _mobileRepository.GetMobileIdAsync(mobileId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _reviewRepository.DeleteReviewList(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the reviews");
            }


            if (!await _mobileRepository.DeleteMobile(mobileToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the mobile");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



    }
}
