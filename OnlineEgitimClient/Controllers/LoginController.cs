using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineEgitimClient.Dtos.AppUserDto;
using OnlineEgitimClient.Service;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace OnlineEgitimClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly INotyfService _notyfService;
        public LoginController(CustomHttpClient customHttpClient, INotyfService notyfService)
        {
            _customHttpClient = customHttpClient;
            _notyfService = notyfService;
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
            //var message = responseObject?["message"]?.ToString();
            var userId = responseObject?["userId"]?.ToString();
       
          
            if (responseMessage.IsSuccessStatusCode)
            {
                Response.Cookies.Append("Token", token ?? "");
                Response.Cookies.Append("TokenExpires", expires ?? "");
                HttpContext.Session.SetString("UserNameOrEmail", model.UserNameOrEmail);
                HttpContext.Session.SetString("userId", userId ?? "");
                _notyfService.Success("Kullanıcı Girişi Başarılı");
                return RedirectToAction("Index", "Default");
            }
            else
            {
                // Hata durumunda konsola hataları yazdırma
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"HTTP Hata Kodu: {responseMessage.StatusCode}");
                Console.WriteLine($"Hata Detayları: {errorContent}");
                _notyfService.Error("Kullanıcı Girişi Başarısız");
                return View();
            }
            //return View();
        }
        [Authorize(AuthenticationSchemes = "Google") ]
        public async Task<IActionResult> GoogleLogin()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userClaims = claimsIdentity?.Claims;

            var userInfo = userClaims?.Select(c => new { c.Type, c.Value });
            var userEmail = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userName = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var userId = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userFirstName = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var userLastName = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            var provider = "Google";
            var accessToken = await HttpContext.GetTokenAsync(GoogleDefaults.AuthenticationScheme, "access_token");


            Console.WriteLine($"accessToken: {accessToken}");
            Console.WriteLine($"User Email: {userEmail}");
            Console.WriteLine($"User Name: {userName}");
            Console.WriteLine($"User ID: {userId}");
            Console.WriteLine($"User First Name: {userFirstName}");
            Console.WriteLine($"User Last Name: {userLastName}");
            Console.WriteLine($"provider: {provider}");
         

            return RedirectToAction("Index", "Login");
        }
        [Authorize(AuthenticationSchemes = "Facebook")]
        public async Task<IActionResult> FacebookLogin()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userClaims = claimsIdentity?.Claims;
            var userInfo = userClaims?.Select(c => new { c.Type, c.Value });

            foreach (var item in userInfo)
            {
                Console.WriteLine($"{item.Type}: {item.Value}");
            }
            
            var userEmail = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userName = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var userId = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userFirstName = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var userLastName = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            var provider = "Facebook";
            var accessToken = await HttpContext.GetTokenAsync(GoogleDefaults.AuthenticationScheme, "access_token");

            var authenticationResult = await HttpContext.AuthenticateAsync("Facebook");

            // Eğer kullanıcı başarılı bir şekilde doğrulandıysa
            if (authenticationResult.Succeeded)
            {
                // Access Token bilgisini alıp konsola yazdırabilirsiniz
                var accesToken = authenticationResult.Properties.GetTokenValue("access_token");

                // Konsola yazdırma işlemi
                Console.WriteLine("Access Token: " + accesToken);

                // Diğer işlemler veya dönüş
                // Örneğin, Access Token'i başka bir servise gönderme veya kullanma işlemleri yapılabilir.
            }
            Console.WriteLine($"accessToken: {accessToken}");
            Console.WriteLine($"User Email: {userEmail}");
            Console.WriteLine($"User Name: {userName}");
            Console.WriteLine($"User ID: {userId}");
            Console.WriteLine($"User First Name: {userFirstName}");
            Console.WriteLine($"User Last Name: {userLastName}");
            Console.WriteLine($"provider: {provider}");


            return RedirectToAction("Index", "Login");
        }
        [Authorize]
        public IActionResult Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); -> cookie ile auth
            //await HttpContext.SignOutAsync(); -> identitiy claim ile çıkış
            HttpContext.Session.Remove("UserNameOrEmail");
            HttpContext.Session.Remove("userId");
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("TokenExpires");
            Response.Cookies.Delete("UserCourseList");
            return RedirectToAction("Index", "Login");
        }
    }
}
