using OnlineEgitimClient.Dtos.AboutDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    }
}
