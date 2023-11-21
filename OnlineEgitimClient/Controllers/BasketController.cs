using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimClient.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
