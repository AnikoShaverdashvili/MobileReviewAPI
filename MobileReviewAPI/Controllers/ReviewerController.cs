using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Models;
using MobileReviewAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MobileReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewerDto>>> GetAllReviewers()
        {
            var reviewers = await _reviewerRepository.GetAllReviewers();
            var mapReviewers = _mapper.Map<IEnumerable<ReviewerDto>>(reviewers);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mapReviewers);
        }

        [HttpGet("{reviewerId}")]
        public async Task<ActionResult<ReviewerDto>> GetReviewerById(int reviewerId)
        {
            if (!_reviewerRepository.ReviwerExists(reviewerId))
            {
                return NotFound();
            }

            var reviewer = await _reviewerRepository.GetReviewerById(reviewerId);
            var mapReviewer = _mapper.Map<ReviewerDto>(reviewer);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mapReviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviwerExists(reviewerId))
            {
                return NotFound();
            }

            var reviews = await _reviewerRepository.GetReviewsByReviewer(reviewerId);
            var mapReviews = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mapReviews);
        }
    }
}
