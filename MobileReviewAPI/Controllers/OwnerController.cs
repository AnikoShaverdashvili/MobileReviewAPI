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
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Category>> GetAllCategories()
        {
            var owners = await _ownerRepository.GetAllOwnersAsync();
            var mapOwners = _mapper.Map<IEnumerable<OwnerDto>>(owners);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapOwners);

        }

        [HttpGet("{ownerId}")]
        public async Task<ActionResult<Owner>> GetCategoryById(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var owner = await _ownerRepository.GetOwnerById(ownerId);
            var mapOwner = _mapper.Map<OwnerDto>(owner);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapOwner);
        }

        [HttpGet("Mobile/{ownerId}")]
        public async Task<ActionResult> GetMobileByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var mobiles = await _ownerRepository.GetMobileByOwner(ownerId);
            if (mobiles == null)
            {
                return NotFound();
            }
            var mapMobiles = _mapper.Map<IEnumerable<MobileDto>>(mobiles);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapMobiles);
        }

    }
}