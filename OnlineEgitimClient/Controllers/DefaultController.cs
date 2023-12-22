using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.AboutDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        public PartialViewResult TestimonialPartial()
        {
            return PartialView();
        }
      
       
    }
}
