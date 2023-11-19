using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);
            var token = responseObject?["value"]?["token"]?.ToString();
            var expires = responseObject?["value"]?["expires"]?.ToString();
            var message = responseObject?["message"]?.ToString();
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("TokenExpires");
            Response.Cookies.Append("Token", token ?? "");
            Response.Cookies.Append("TokenExpires", expires ?? "");
            //Console.WriteLine(token);
            //Console.WriteLine(expires);
            //Console.WriteLine(message);
            //gelen veri
            //{
            //  "message": "Kullanıcı girişi başarılı.",
            //  "value": {
            //      "token":                      "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MDAzMDgzNTAsImV4cCI6MTcwMDMwODUzMCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3QiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdCJ9.0DuJEZTBRHT5dk7P4r5qbUvONqgnlChUZ7Qqss_9Fho",
            //      "expires": "2023-11-18T14:55:30.4090744+03:00"
            //  }
            //}
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
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
