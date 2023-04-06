using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MobileReviewAPI.Data;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Interfaces;
using MobileReviewAPI.Models;
using MobileReviewAPI.Repositories;

namespace MobileReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoutryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CoutryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Country>> GetCountries()
        {
            var countries = await _countryRepository.GetAllCountriesAsync();
            var countriesDto =_mapper.Map<IEnumerable<CountryDto>>(countries);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(countriesDto);
        }
        [HttpGet("{countryId}")]
        public async Task<ActionResult<Country>>GetCountryById(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }
            var country = await _countryRepository.GetCountryById(countryId);
            var mapCountry=_mapper.Map<CountryDto>(country);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapCountry);
        }
        [HttpGet("Owner/{ownerId}")]
        public async Task<ActionResult<Country>>GetCountryOfOwner(int ownerId)
        {
            var country = await _countryRepository.GetCountryByOwner(ownerId);
            var mapcountry = _mapper.Map<CountryDto>(country);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mapcountry);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCountry(CountryDto createCountry)
        {
            if (createCountry == null)
            {
                return BadRequest(ModelState);
            }
            var countries = await _countryRepository.GetAllCountriesAsync();
            var countryExists = countries.FirstOrDefault(c => c.Name.Trim().ToUpper() == createCountry.Name.TrimEnd().ToUpper());

            if (countryExists != null)
            {
                ModelState.AddModelError("", "Country already exists.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var countryMap = _mapper.Map<Country>(createCountry);
            if (!await _countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpPut("{countryId}")]
        public async Task<IActionResult> UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null)
            {
                return BadRequest(ModelState);
            }

            if (countryId != updatedCountry.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var countryMap = _mapper.Map<Country>(updatedCountry);

            if (!await _countryRepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{countryId}")]
        public async Task<ActionResult> DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }
            var countryToDelete = await _countryRepository.GetCountryById(countryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting country");
            }
            return NoContent();
        }

    }
}
