using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Interfaces;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
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

        [HttpPost]
        public async Task<ActionResult> CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto createOwner)
        {
            if (createOwner == null)
            {
                return BadRequest(ModelState);
            }

            var owners = await _ownerRepository.GetAllOwnersAsync();
            var ownersExists = owners.FirstOrDefault(o => o.LastName.Trim().ToUpper() == createOwner.LastName.TrimEnd().ToUpper());
            if (ownersExists != null)
            {
                ModelState.AddModelError("CategoryName", "Owner already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = _mapper.Map<Owner>(createOwner);
            ownerMap.Country = await _countryRepository.GetCountryById(countryId);

            if (!await _ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{ownerId}")]
        public async Task<IActionResult> UpdateOwner(int ownerId, [FromBody] OwnerDto updatedOwner)
        {
            if (updatedOwner == null)
            {
                return BadRequest(ModelState);
            }

            if (ownerId != updatedOwner.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ownerMap = _mapper.Map<Owner>(updatedOwner);

            if (!await _ownerRepository.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{ownerId}")]
        public async Task<IActionResult> DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var ownerToDelete = await _ownerRepository.GetOwnerById(ownerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _ownerRepository.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the owner");
            }

            return NoContent();
        }


    }
}