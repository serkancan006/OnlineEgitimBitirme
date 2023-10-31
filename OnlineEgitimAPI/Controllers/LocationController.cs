using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.LocationDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _LocationService;
        private readonly IMapper _mapper;
        public LocationController(ILocationService LocationService, IMapper mapper)
        {
            _LocationService = LocationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult LocationList()
        {
            var values = _LocationService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddLocation(AddLocationDto addLocationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<Location>(addLocationDto);
            _LocationService.TAdd(values);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
            var values = _LocationService.TGetByID(id);
            _LocationService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateLocation(UpdateLocationDto updateLocationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<Location>(updateLocationDto);
            _LocationService.TUpdate(values);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetLocation(int id)
        {
            var values = _LocationService.TGetByID(id);
            return Ok(values);
        }
    }
}
