using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.PurchasedCourseDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedCourseController : ControllerBase
    {
        private readonly IPurchasedCourseService _PurchasedCourseService;
        private readonly IMapper _mapper;
        public PurchasedCourseController(IPurchasedCourseService PurchasedCourseService, IMapper mapper)
        {
            _PurchasedCourseService = PurchasedCourseService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult PurchasedCourseList()
        {
            var values = _PurchasedCourseService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddPurchasedCourse(AddPurchasedCourseDto addPurchasedCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<PurchasedCourse>(addPurchasedCourseDto);
            _PurchasedCourseService.TAdd(values);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePurchasedCourse(int id)
        {
            var values = _PurchasedCourseService.TGetByID(id);
            _PurchasedCourseService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdatePurchasedCourse(UpdatePurchasedCourseDto updatePurchasedCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<PurchasedCourse>(updatePurchasedCourseDto);
            _PurchasedCourseService.TUpdate(values);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetPurchasedCourse(int id)
        {
            var values = _PurchasedCourseService.TGetByID(id);
            return Ok(values);
        }
    }
}
