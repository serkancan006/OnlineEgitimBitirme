using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimClient.Controllers
{
    public class PurchasedCourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CourseContent()
        {
            return View();
        }
    }
}
