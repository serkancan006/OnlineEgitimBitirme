using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.AppUserDto;
using OnlineEgitimClient.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineEgitimClient.Controllers
{
    public class RegisterController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public RegisterController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterAppUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var responseMessage = await _customHttpClient.Post<RegisterAppUserDto>(new() { Controller = "Register" }, model);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index","Login");
            else
            {
                // Hata durumunda konsola hataları yazdırma
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"HTTP Hata Kodu: {responseMessage.StatusCode}");
                Console.WriteLine($"Hata Detayları: {errorContent}");
                var errors = JsonConvert.DeserializeObject<List<IdentityError>>(errorContent);
                foreach(var error in errors ?? Enumerable.Empty<IdentityError>())
{
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            //return View();
        }

        [HttpGet]
        public IActionResult InstructorRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InstructorRegister(RegisterAppUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var responseMessage = await _customHttpClient.Post<RegisterAppUserDto>(new() { Controller = "Register", Action= "AddUserWithRole" }, model);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index", "Login");
            else
            {
                // Hata durumunda konsola hataları yazdırma
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"HTTP Hata Kodu: {responseMessage.StatusCode}");
                Console.WriteLine($"Hata Detayları: {errorContent}");
                var errors = JsonConvert.DeserializeObject<List<IdentityError>>(errorContent);
                foreach (var error in errors ?? Enumerable.Empty<IdentityError>())
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            //return View();
        }
    }
}
