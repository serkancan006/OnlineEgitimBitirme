using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet]
        public IActionResult LocationList()
        {
            var values = _locationService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddLocation(Location location)
        {
            _locationService.TAdd(location);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
            var values = _locationService.TGetByID(id);
            _locationService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateLocation(Location location)
        {
            _locationService.TUpdate(location);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetLocation(int id)
        {
            var values = _locationService.TGetByID(id);
            return Ok(values);
        }
    }
}
