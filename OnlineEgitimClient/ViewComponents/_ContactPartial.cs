using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.ContactDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.ViewComponents
{
    public class _ContactPartial : ViewComponent
    {
        private readonly CustomHttpClient _customHttpClient;
        public _ContactPartial(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
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


    }
}
