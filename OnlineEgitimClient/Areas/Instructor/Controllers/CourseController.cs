using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Dtos.LocationDto;
using OnlineEgitimClient.Service;
using System.Text;

namespace OnlineEgitimClient.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class CourseController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly IMapper _mapper;
        public CourseController(CustomHttpClient customHttpClient, IMapper mapper)
        {
            _customHttpClient = customHttpClient;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

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
        public async Task<IActionResult> AddCourse()
        {
            ViewBag.UserID = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Location", Action = "LocationListByStatus" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListLocationDto>>(jsonData);
                ViewBag.LocationList = new SelectList(values, "Id", "Address");
                return View();
            }
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

        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course" }, id);
            var responseMessage2 = await _customHttpClient.Get(new() { Controller = "Location", Action = "LocationListByStatus" });
            if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCourseListDto>(jsonData);
                var result = _mapper.Map<UpdateCourseDto>(values);
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<List<ListLocationDto>>(jsonData2);
                ViewBag.LocationList = new SelectList(values2, "Id", "Address");
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto model)
        {
            var responseMessage = await _customHttpClient.PutMultipartFormData<UpdateCourseDto>(new() { Controller = "Course" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
