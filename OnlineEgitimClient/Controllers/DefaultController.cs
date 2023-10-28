using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimClient.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }
        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
        public PartialViewResult About2Partial()
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
    }
}
