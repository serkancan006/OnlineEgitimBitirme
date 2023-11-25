using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Dtos.CourseVideoDto;
using OnlineEgitimClient.Service;
using System.Data;

namespace OnlineEgitimClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public CourseController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course" });
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var responseMessage = await _customHttpClient.Post<AddCourseDto>(new() { Controller = "Course" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var responseMessage = await _customHttpClient.Delete(new() { Controller = "Course" }, id);
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
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCourseDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto model)
        {
            var responseMessage = await _customHttpClient.Put<UpdateCourseDto>(new() { Controller = "Course" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CourseVideos(int id)
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
                return RedirectToAction("CourseVideos", new { id = id });
            }
            return View();
        }

    }
}
