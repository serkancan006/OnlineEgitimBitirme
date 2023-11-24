using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.PurchasedCourseDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    [Authorize]
    public class PurchasedCourseController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public PurchasedCourseController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }
        //https://localhost:7064/api/PurchasedCourse/PurchasedCourseList?username=serkan06
        public async Task<IActionResult> Index()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "PurchasedCourse", Action = "PurchasedCourseList", QueryString = $"username={HttpContext.Session.GetString("UserNameOrEmail")}" });

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListPurchasedCourseWİthCourseDto>>(jsonData);
                return View(values);
            }

            return View();
        }
        public IActionResult CourseContent()
        {
            return View();
        }
    }
}
