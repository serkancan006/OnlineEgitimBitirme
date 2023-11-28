using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.AboutDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.ViewComponents
{
    public class _AboutPartial : ViewComponent
    {
        private readonly CustomHttpClient _customHttpClient;

        public _AboutPartial(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "About" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListAboutDto>>(jsonData);
                return View(values);
            }
            return View();
        }

    }
}
