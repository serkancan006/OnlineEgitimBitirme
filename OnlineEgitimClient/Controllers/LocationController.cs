using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.LocationDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class LocationController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public LocationController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }
        public async Task<IActionResult> AdminIndex()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Location" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListLocationDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult AdminAddLocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminAddLocation(AddLocationDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var responseMessage = await _customHttpClient.Post<AddLocationDto>(new() { Controller = "Location" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        public async Task<IActionResult> AdminDeleteLocation(int id)
        {
            var responseMessage = await _customHttpClient.Delete(new() { Controller = "Location" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminUpdateLocation(int id)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Location" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateLocationDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminUpdateLocation(UpdateLocationDto model)
        {
            var responseMessage = await _customHttpClient.Put<UpdateLocationDto>(new() { Controller = "Location" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }
    }
}
