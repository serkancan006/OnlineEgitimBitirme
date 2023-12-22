using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using OnlineEgitimClient.Models;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly INotyfService _notyfService;
        public ContactUsController(CustomHttpClient customHttpClient, INotyfService notyfService)
        {
            _customHttpClient = customHttpClient;
            _notyfService = notyfService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactUsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index","Contact");
            }

            var responseMessage = await _customHttpClient.Post<ContactUsViewModel>(new() { Controller = "ContactUs" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                _notyfService.Success("Mesajınız Gönderildi. En kısa sürede mailinize dönüş yapılacaktır.");
                return RedirectToAction("Index","Default");
            }
            else
            {
                _notyfService.Error("Mesajınız Gönderilemedi.");
                return RedirectToAction("Index", "Contact");
            }
        }
    }
}
