using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Service;
using System.Text;

namespace OnlineEgitimClient.ViewComponents
{
    public class _Top5PurchasedCourse : ViewComponent
    {
        private readonly CustomHttpClient _customHttpClient;
        public _Top5PurchasedCourse(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Statistic", Action = "MostPurchasedCourses" });

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<TopPurchasedCourseDto>>(jsonData);
                return View(values);
            }
            return View();
        }

    }
}
