using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Service;
using System.Text;

namespace OnlineEgitimClient.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class CourseController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public CourseController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(Encoding.UTF8.GetString(HttpContext.Session.Get("userId")));

            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course", Action= "CourseListByInstructor" }, userId);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListCourseDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            ViewBag.UserID = Convert.ToInt32(Encoding.UTF8.GetString(HttpContext.Session.Get("userId")));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var responseMessage = await _customHttpClient.PostMultipartFormData<AddCourseDto>(new() { Controller = "Course" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
