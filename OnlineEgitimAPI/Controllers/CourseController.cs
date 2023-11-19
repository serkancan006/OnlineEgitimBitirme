using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.CourseDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [Authorize(Roles = "Admin,Instructor")]
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var values = _CourseService.TGetByID(id);
            _CourseService.TDelete(values);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
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
        [HttpGet("[action]")]
        public IActionResult CourseListByStatus()
        {
            var values = _CourseService.TGetListTrueStatus();
            return Ok(values);
        }
    }
}
