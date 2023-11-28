using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.AboutDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class DefaultController : Controller
    {
      
        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }
        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
        public PartialViewResult NewCoursesPartial()
        {
            return PartialView();
        }
        public PartialViewResult InstructorsPartial()
        {
            return PartialView();
        }
        public PartialViewResult TestimonialPartial()
        {
            return PartialView();
        }
        public PartialViewResult ContactPartial()
        {
            return PartialView();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
