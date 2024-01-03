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
using Microsoft.AspNetCore.Authentication.Facebook;

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
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);
                var token = responseObject?["value"]?["token"]?.ToString();
                var expires = responseObject?["value"]?["expires"]?.ToString();
                //var message = responseObject?["message"]?.ToString();
                var userId = responseObject?["userId"]?.ToString();
                //
                // Oturum bilgilerini al
                var existingToken = HttpContext.Request.Cookies["Token"];
                var existingExpires = HttpContext.Request.Cookies["TokenExpires"];

                // Eğer yeni bir token varsa veya token süresi değiştiyse, oturum ve token bilgilerini güncelle
                if (token != existingToken || expires != existingExpires)
                {
                    // Yeni token ve süresini çerezlere kaydet
                    Response.Cookies.Append("Token", token ?? "", new CookieOptions { SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("TokenExpires", expires ?? "", new CookieOptions { SameSite = SameSiteMode.Strict });

                    // Oturum verilerini güncelle
                    HttpContext.Session.SetString("UserNameOrEmail", model.UserNameOrEmail);
                    HttpContext.Session.SetString("userId", userId ?? "");
                }

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
            var authenticationResult = await HttpContext.AuthenticateAsync("Google");

            if (authenticationResult.Succeeded)
            {
                ExternalLoginDto user = new ExternalLoginDto();
                var claimsIdentity = User.Identity as ClaimsIdentity;

                user.userEmail = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                user.userName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                user.userId = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                user.userFirstName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                user.userLastName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                user.provider = claimsIdentity?.AuthenticationType;
                user.accessToken = await HttpContext.GetTokenAsync(GoogleDefaults.AuthenticationScheme, "access_token");

                var responseMessage = await _customHttpClient.Post<ExternalLoginDto>(new() { Controller = "Login", Action="GoogleLogin" }, user);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);
                    var token = responseObject?["value"]?["token"]?.ToString();
                    var expires = responseObject?["value"]?["expires"]?.ToString();
                    //var message = responseObject?["message"]?.ToString();
                    var userId = responseObject?["userId"]?.ToString();
                    //
                    // Oturum bilgilerini al
                    var existingToken = HttpContext.Request.Cookies["Token"];
                    var existingExpires = HttpContext.Request.Cookies["TokenExpires"];
                    // Eğer yeni bir token varsa veya token süresi değiştiyse, oturum ve token bilgilerini güncelle
                    if (token != existingToken || expires != existingExpires)
                    {
                        // Yeni token ve süresini çerezlere kaydet
                        Response.Cookies.Append("Token", token ?? "", new CookieOptions { SameSite = SameSiteMode.Strict });
                        Response.Cookies.Append("TokenExpires", expires ?? "", new CookieOptions { SameSite = SameSiteMode.Strict });

                        // Oturum verilerini güncelle
                        HttpContext.Session.SetString("UserNameOrEmail", user.userName);
                        HttpContext.Session.SetString("userId", userId ?? "");
                    }
                    _notyfService.Success("Kullanıcı Girişi Başarılı");
                    return RedirectToAction("Index", "Default");
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [Authorize(AuthenticationSchemes = "Facebook")]
        public async Task<IActionResult> FacebookLogin()
        {
            var authenticationResult = await HttpContext.AuthenticateAsync("Facebook");
            if (authenticationResult.Succeeded)
            {
                ExternalLoginDto user = new ExternalLoginDto();
                var claimsIdentity = User.Identity as ClaimsIdentity;

                user.userEmail = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                user.userName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                user.userId = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                user.userFirstName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                user.userLastName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                user.provider = claimsIdentity?.AuthenticationType;
                user.accessToken = await HttpContext.GetTokenAsync(FacebookDefaults.AuthenticationScheme, "access_token");

                var responseMessage = await _customHttpClient.Post<ExternalLoginDto>(new() { Controller = "Login", Action = "FacebookLogin" }, user);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);
                    var token = responseObject?["value"]?["token"]?.ToString();
                    var expires = responseObject?["value"]?["expires"]?.ToString();
                    //var message = responseObject?["message"]?.ToString();
                    var userId = responseObject?["userId"]?.ToString();
                    //
                    // Oturum bilgilerini al
                    var existingToken = HttpContext.Request.Cookies["Token"];
                    var existingExpires = HttpContext.Request.Cookies["TokenExpires"];
                    // Eğer yeni bir token varsa veya token süresi değiştiyse, oturum ve token bilgilerini güncelle
                    if (token != existingToken || expires != existingExpires)
                    {
                        // Yeni token ve süresini çerezlere kaydet
                        Response.Cookies.Append("Token", token ?? "", new CookieOptions { SameSite = SameSiteMode.Strict });
                        Response.Cookies.Append("TokenExpires", expires ?? "", new CookieOptions { SameSite = SameSiteMode.Strict });

                        // Oturum verilerini güncelle
                        HttpContext.Session.SetString("UserNameOrEmail", user.userName);
                        HttpContext.Session.SetString("userId", userId ?? "");
                    }
                    _notyfService.Success("Kullanıcı Girişi Başarılı");
                    return RedirectToAction("Index", "Default");
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [Authorize]
        public IActionResult Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); -> cookie ile auth
            //await HttpContext.SignOutAsync(); -> identitiy claim ile çıkış
            HttpContext.Session.Remove("UserNameOrEmail");
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("TokenExpires");
            Response.Cookies.Delete("UserCourseList"); //  , new CookieOptions { SameSite = SameSiteMode.Strict }
            return RedirectToAction("Index", "Login");
        }
    }
}
