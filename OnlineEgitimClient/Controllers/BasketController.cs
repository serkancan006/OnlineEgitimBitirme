using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEgitimClient.Dtos.PurchasedCourseDto;
using OnlineEgitimClient.Service;
using System.Text;

namespace OnlineEgitimClient.Controllers
{
    public class BasketController : Controller
    {
        private readonly BasketService _basketService;
        private readonly CustomHttpClient _customHttpClient;

        public BasketController(BasketService basketService, CustomHttpClient customHttpClient)
        {
            _basketService = basketService;
            _customHttpClient = customHttpClient;
        }

        public IActionResult Index()
        {
            var values = _basketService.GetBasketCourse();
            ViewBag.totalPrice = _basketService.TotalPrice();
            ViewBag.totalCourse = _basketService.TotalCourse();
            return View(values);
        }
        //[Authorize]
        //public async Task<IActionResult> Buy()
        //{
        //    var userId = Convert.ToInt32(Encoding.UTF8.GetString(HttpContext.Session.Get("userId")));
        //    await _basketService.BuyCourse(userId);
        //    return RedirectToAction("Index");
        //}
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