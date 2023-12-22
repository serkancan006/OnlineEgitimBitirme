using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using OnlineEgitimClient.Models;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.ViewComponents
{
    public class _ContactUsPartial : ViewComponent
    {

        [HttpGet]
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
