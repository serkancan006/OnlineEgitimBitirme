using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Dtos.CourseVideoDto;
using OnlineEgitimClient.Service;
using System.Text;

namespace OnlineEgitimClient.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class DashboardController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public DashboardController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(Encoding.UTF8.GetString(HttpContext.Session.Get("userId")));

            var responseMessage = await _customHttpClient.Get(new() { Controller = "Statistic", Action= "InstructorCourseCount" }, userId);
            var responseMessage2 = await _customHttpClient.Get(new() { Controller = "Statistic", Action = "InstructorPurchasedCourseCount" }, userId);
            var responseMessage3 = await _customHttpClient.Get(new() { Controller = "Statistic", Action = "InstructorTotalViewCount" }, userId);
            var responseMessage4 = await _customHttpClient.Get(new() { Controller = "Statistic", Action = "InstructorMostViewCourse" }, userId);

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();

            var value = JsonConvert.DeserializeObject<List<ListCourseDto>>(jsonData4);

            ViewBag.CourseCount = jsonData;
            ViewBag.PurchasedCourseCount = jsonData2;
            ViewBag.TotalViewCount = jsonData3;

            return View(value);
        }
    }
}
