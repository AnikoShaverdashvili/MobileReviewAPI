using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Models;
using MobileReviewAPI.Repositories;

namespace MobileReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
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
    }
}
