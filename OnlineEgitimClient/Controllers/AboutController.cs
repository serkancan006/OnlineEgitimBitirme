using Microsoft.AspNetCore.Mvc;

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
