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
       
        public PartialViewResult InstructorsPartial()
        {
            return PartialView();
        }
        public PartialViewResult TestimonialPartial()
        {
            return PartialView();
        }
      
       
    }
}
