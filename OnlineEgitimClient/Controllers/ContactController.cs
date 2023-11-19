using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.ContactDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class ContactController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public ContactController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public IActionResult Index()
        {
            return View();
        }
      
    }
}
