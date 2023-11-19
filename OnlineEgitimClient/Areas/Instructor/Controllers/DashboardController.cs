using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimClient.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
