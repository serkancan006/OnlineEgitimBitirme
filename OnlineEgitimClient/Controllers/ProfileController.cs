using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.AppUserDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public ProfileController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userid = HttpContext.Session.GetString("userId");
            var responseMessage = await _customHttpClient.Get(new() { Controller = "User" }, Convert.ToInt32(userid));

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<AppUserDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserDto model)
        {
            model.UserId = HttpContext.Session.GetString("userId");
            var responseMessage = await _customHttpClient.Post<AppUserDto>(new() { Controller = "User" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Default");
            }
            return View();
        }

    }
}
