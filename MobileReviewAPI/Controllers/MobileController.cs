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
    public class MobileController : ControllerBase
    {
        private readonly IMobileRepository _mobileRepository;
        private readonly IMapper _mapper;
        public MobileController(IMobileRepository mobileRepository, IMapper mapper)
        {
            _mobileRepository = mobileRepository;
            _mapper = mapper;
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


    }
}
