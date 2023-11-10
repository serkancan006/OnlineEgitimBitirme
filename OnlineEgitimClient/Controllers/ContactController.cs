using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.ContactDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.Controllers
{
    public class ContactController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public ContactController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AdminIndex()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Contact" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListContactDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult AdminAddContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminAddContact(AddContactDto model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var responseMessage = await _customHttpClient.Post<AddContactDto>(new() { Controller = "Contact" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        public async Task<IActionResult> AdminDeleteContact(int id)
        {
            var responseMessage = await _customHttpClient.Delete(new() { Controller = "Contact" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminUpdateContact(int id)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Contact" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateContactDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminUpdateContact(UpdateContactDto model)
        {
            var responseMessage = await _customHttpClient.Put<UpdateContactDto>(new() { Controller = "Contact" }, model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }
    }
}
