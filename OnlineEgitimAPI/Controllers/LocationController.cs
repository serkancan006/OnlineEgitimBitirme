using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.LocationDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        [Authorize]
        [HttpGet]
        public IActionResult LocationList()
        {
            var values = _LocationService.TGetList();
            return Ok(values);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
            var values = _LocationService.TGetByID(id);
            _LocationService.TDelete(values);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetLocation(int id)
        {
            var values = _LocationService.TGetByID(id);
            return Ok(values);
        }
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult LocationListByStatus()
        {
            var values = _LocationService.TGetListTrueStatus();
            return Ok(values);
        }
    }
}
