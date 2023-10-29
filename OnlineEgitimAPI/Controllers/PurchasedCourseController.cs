using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedCourseController : ControllerBase
    {
        private readonly IPurchasedCourseService _purchasedCourseService;

        public PurchasedCourseController(IPurchasedCourseService purchasedCourseService)
        {
            _purchasedCourseService = purchasedCourseService;
        }
        [HttpGet]
        public IActionResult PurchasedCourseList()
        {
            var values = _purchasedCourseService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddPurchasedCourse(PurchasedCourse purchasedCourse)
        {
            _purchasedCourseService.TAdd(purchasedCourse);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeletePurchasedCourse(int id)
        {
            var values = _purchasedCourseService.TGetByID(id);
            _purchasedCourseService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdatePurchasedCourse(PurchasedCourse purchasedCourse)
        {
            _purchasedCourseService.TUpdate(purchasedCourse);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetPurchasedCourse(int id)
        {
            var values = _purchasedCourseService.TGetByID(id);
            return Ok(values);
        }
    }
}
