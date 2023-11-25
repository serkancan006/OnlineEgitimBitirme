using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseVideoDto;
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
        //https://localhost:7064/api/CourseVideoFile/GetCourseVideoFileWithUser?courseId=5&username=serkan006
        public async Task<IActionResult> CourseContent(int id)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "CourseVideoFile", Action = "GetCourseVideoFileWithUser", QueryString = $"courseId={id}&username={HttpContext.Session.GetString("UserNameOrEmail")}" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListCourseVideoDto>>(jsonData);
                return View(values);
            }
            return View();
        }
      
    }
}
