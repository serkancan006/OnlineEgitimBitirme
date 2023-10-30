using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public IActionResult CourseList()
        {
            var values = _courseService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            _courseService.TAdd(course);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var values = _courseService.TGetByID(id);
            _courseService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateCourse(Course course)
        {
            _courseService.TUpdate(course);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            var values = _courseService.TGetByID(id);
            return Ok(values);
        }
    }
}
