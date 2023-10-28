using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimClient.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CourseDetails(int id)
        {
            return View();
        }

    }
}
