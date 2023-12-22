using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.ViewComponents
{
    public class _Top10InstructorList : ViewComponent
    {
        private readonly CustomHttpClient _customHttpClient;

        public _Top10InstructorList(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "User", Action = "InstructorList" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AppUser>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
