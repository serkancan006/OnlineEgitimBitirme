using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class CourseController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public CourseController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CourseDetails(int id)
        {
            return View();
        }
        public PartialViewResult RelatedCourses()
        {
            return PartialView();
        }
    }
}
