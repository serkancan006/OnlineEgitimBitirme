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


        public IActionResult Index()
        {
            return View();
        }
    }
}
