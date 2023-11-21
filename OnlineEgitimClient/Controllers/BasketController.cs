using Microsoft.AspNetCore.Mvc;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class BasketController : Controller
    {
        private readonly BasketService _basketService;
        public BasketController(BasketService basketService)
        {
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            var values = _basketService.GetBasketCourse();
            ViewBag.totalPrice = _basketService.TotalPrice();
            ViewBag.totalCourse = _basketService.TotalCourse();
            return View(values);
        }
        public async Task<IActionResult> AddToCourse(int courseId)
        {
            await _basketService.AddBasketCourse(courseId);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteToCourse(int id)
        {
            _basketService.DeleteBasketCourse(id);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveToCourse()
        {
            _basketService.ClearBasketCourse();
            return RedirectToAction("Index");
        }
    }
}
