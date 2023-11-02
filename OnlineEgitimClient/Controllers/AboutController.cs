using OnlineEgitimClient.Dtos.AboutDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Models.About;
using System.Text;
using OnlineEgitimClient.Service;
using System.Net.Http;

namespace OnlineEgitimClient.Controllers
{
    public class AboutController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public AboutController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AdminIndex()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "About" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListAboutDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult AdminAddAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminAddAbout(AddAboutDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var responseMessage = await _customHttpClient.Post<AddAboutDto>(new() { Controller = "About" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        public async Task<IActionResult> AdminDeleteAbout(int id)
        {
            var responseMessage = await _customHttpClient.Delete(new() { Controller="About" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminUpdateAbout(int id)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "About" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminUpdateAbout(UpdateAboutDto model)
        {
            var responseMessage = await _customHttpClient.Put<UpdateAboutDto>(new() { Controller = "About" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }
    }
}
