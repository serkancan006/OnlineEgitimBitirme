using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Service;
using System.Data;
using System.Text;

namespace OnlineEgitimClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public DashboardController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Statistic", Action = "CourseCount" });
            var responseMessage2 = await _customHttpClient.Get(new() { Controller = "Statistic", Action = "PurchasedCourseCount" });
            var responseMessage3 = await _customHttpClient.Get(new() { Controller = "Statistic", Action = "TotalViewCount" });
            var responseMessage4 = await _customHttpClient.Get(new() { Controller = "Statistic", Action = "MostViewCourse" });

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
