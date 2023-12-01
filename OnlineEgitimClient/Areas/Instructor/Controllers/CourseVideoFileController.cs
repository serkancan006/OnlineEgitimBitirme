using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseVideoDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class CourseVideoFileController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public CourseVideoFileController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IActionResult> Index(int id)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "CourseVideoFile" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListCourseVideoDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CourseUploadVideo(int id)
        {
            ViewBag.CourseID = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CourseUploadVideo(IFormFile file, int id)
        {
            var responseMessage = await _customHttpClient.PostFile(new RequestParameters { Controller = "CourseVideoFile", QueryString = $"id={id}" }, file);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new { id = id });
            }
            return View();
        }
        public async Task<IActionResult> DeleteCourseVideo(int id, int courseId)
        {
            var responseMessage = await _customHttpClient.Delete(new() { Controller = "CourseVideoFile" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new { id = courseId });
            }
            return View();
        }

    }
}
