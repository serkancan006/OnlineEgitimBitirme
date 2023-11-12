using Microsoft.AspNetCore.Mvc;
using OnlineEgitimClient.Dtos.AppUserDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public LoginController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginAppUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var responseMessage = await _customHttpClient.Post<LoginAppUserDto>(new() { Controller = "Login" }, model);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index", "Default");
            else
            {
                // Hata durumunda konsola hataları yazdırma
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"HTTP Hata Kodu: {responseMessage.StatusCode}");
                Console.WriteLine($"Hata Detayları: {errorContent}");

                return View();
            }
            //return View();
        }
    }
}
