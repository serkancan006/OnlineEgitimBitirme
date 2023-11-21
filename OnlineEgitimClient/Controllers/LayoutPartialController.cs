using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class LayoutPartialController : Controller
    {
        private readonly BasketService _basketService;
        public LayoutPartialController(BasketService basketService)
        {
            _basketService = basketService;
        }

        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult TopbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult NavbarPartial()
        {
            ViewBag.totalCourse = _basketService.TotalCourse();
            return PartialView();
        }
        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        public PartialViewResult ScriptPartial()
        {
            return PartialView();
        }
    }
}
