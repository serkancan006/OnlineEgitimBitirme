using AutoMapper;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Dtos.WidgetClickLogDto;
using OnlineEgitimClient.Service;
using System.Text;

namespace OnlineEgitimClient.Controllers
{
    public class CourseController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public CourseController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course", Action = "CourseListByStatus" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListCourseDto>>(jsonData);
                return View(values);
            }

            return View();
        }
      
        public async Task<IActionResult> CourseDetails(int id)
        {
            var userId = HttpContext.Session.Get("userId");
            if (userId != null)
            {
                AddWidgetClickLogDto model = new AddWidgetClickLogDto();
                model.CourseID = id;
                model.AppUserID = Convert.ToInt32(Encoding.UTF8.GetString(userId));
                await _customHttpClient.Post(new() { Controller = "WidgetClickLog" }, model);
            }

            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course", Action= "GetCourseByUser" },id);
            
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ListCourseDto>(jsonData);
                return View(values);
            }

            return View();
        }
        public PartialViewResult RelatedCourses()
        {
            return PartialView();
        }
    }
}
