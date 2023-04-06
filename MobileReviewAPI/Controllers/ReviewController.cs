using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Interfaces;
using MobileReviewAPI.Models;
using MobileReviewAPI.Repositories;

namespace MobileReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMobileRepository _mobileRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IMobileRepository mobileRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _mobileRepository = mobileRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Review>> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviews();
            var mapReviews = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapReviews);
        }

        [HttpGet("{reviewId}")]

        public async Task<ActionResult<Review>> GetReviewById(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = await _reviewRepository.GetReviewById(reviewId);
            var mapReview = _mapper.Map<ReviewDto>(review);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapReview);
        }

        [HttpGet("Reviews/{mobId}")]

        public async Task<ActionResult<Review>> GetReviewByMobile(int mobId)
        {
            if (!_reviewRepository.ReviewExists(mobId))
            {
                return NotFound();
            }
            var mobReview = await _reviewRepository.GetMobileReviews(mobId);
            var mapMobRev = _mapper.Map<IEnumerable<ReviewDto>>(mobReview);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapMobRev);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview([FromQuery]int reviewerId,[FromQuery]int mobId,[FromBody] ReviewDto createReview)
        {
            if (createReview == null)
            {
                return BadRequest(ModelState);
            }

            var reviews = await _reviewRepository.GetAllReviews();
            var reviewExists = reviews.FirstOrDefault(m => m.Title.Trim().ToUpper() == createReview.Title.TrimEnd().ToUpper());
            if (reviewExists != null)
            {
                ModelState.AddModelError("", "Review already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(createReview);
            reviewMap.Mobile =await _mobileRepository.GetMobileIdAsync(mobId);
            reviewMap.Reviewer = await _reviewerRepository.GetReviewerById(reviewerId);

            if (!await _reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }


        [HttpPut("{reviewId}")]
        public async Task<ActionResult> UpdateReview(int reviewId, ReviewDto updateReview)
        {
            if (updateReview == null)
            {
                return BadRequest(ModelState);
            }
            if (reviewId != updateReview.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewMap = _mapper.Map<Review>(updateReview);
            if (!await _reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var reviewToDelete = await _reviewRepository.GetReviewById(reviewId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
            }
            return NoContent();
        }


    }
}
