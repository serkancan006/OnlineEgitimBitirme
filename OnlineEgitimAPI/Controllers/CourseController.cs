using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.CourseDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _CourseService;
        private readonly IMapper _mapper;
        public CourseController(ICourseService CourseService, IMapper mapper)
        {
            _CourseService = CourseService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CourseList()
        {
            var values = _CourseService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddCourse(AddCourseDto addCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<Course>(addCourseDto);
            _CourseService.TAdd(values);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var values = _CourseService.TGetByID(id);
            _CourseService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<Course>(updateCourseDto);
            _CourseService.TUpdate(values);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            var values = _CourseService.TGetByID(id);
            return Ok(values);
        }
    }
}
