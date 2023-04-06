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
using MobileReviewAPI.Interfaces;

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
        [HttpPost]
        public async Task<ActionResult> CreateReviwer([FromBody] ReviewerDtoNoReview reviewerCreate)
        {
            if (reviewerCreate == null)
            {
                return BadRequest(ModelState);
            }

            var reviewers = await _reviewerRepository.GetAllReviewers();
            var reviewerExists = reviewers.FirstOrDefault(o => o.LastName.Trim().ToUpper() == reviewerCreate.LastName.TrimEnd().ToUpper());
            if (reviewerExists != null)
            {
                ModelState.AddModelError("", "Reviewer already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapReviewer = _mapper.Map<Reviewer>(reviewerCreate);
          
            if (!await _reviewerRepository.CreateReviewer(mapReviewer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }


        [HttpPut("{reviewerId}")]
        public async Task<ActionResult> UpdateReviewer(int reviewerId, ReviewerDtoNoReview updateReviewer)
        {
            if (updateReviewer == null)
            {
                return BadRequest(ModelState);
            }
            if (reviewerId != updateReviewer.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.ReviwerExists(reviewerId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewerMap = _mapper.Map<Reviewer>(updateReviewer);
            if (!await _reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();


        }

        [HttpDelete("{reviewerId}")]
        public async Task<IActionResult> DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviwerExists(reviewerId))
            {
                return NotFound();
            }

            var reviewerToDelete = await _reviewerRepository.GetReviewerById(reviewerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _reviewerRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
