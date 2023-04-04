using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MobileReviewAPI.Data;
using MobileReviewAPI.DTO;
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

        //[HttpGet("Owners/{countryId}")]
        //public async Task<ActionResult<Country>>GetOwnersFromCountry(int countryId)
        //{
        //    var owner =await _countryRepository.GetOwnersFromCountry(countryId);
        //    var mapOwner=_mapper.Map<OwnerDto>

        //}

    }
}
