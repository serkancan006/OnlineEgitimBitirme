﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AdminIndex()
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
        public IActionResult AdminAddCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminAddCourse(AddCourseDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var responseMessage = await _customHttpClient.Post<AddCourseDto>(new() { Controller = "Course" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }
        public async Task<IActionResult> AdminDeleteCourse(int id)
        {
            var responseMessage = await _customHttpClient.Delete(new() { Controller = "Course" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminUpdateCourse(int id)
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
        public async Task<IActionResult> AdminUpdateCourse(UpdateCourseDto model)
        {
            var responseMessage = await _customHttpClient.Put<UpdateCourseDto>(new() { Controller = "Course" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
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
