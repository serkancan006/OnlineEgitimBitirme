using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Service;
using System.Text;

namespace OnlineEgitimClient.ViewComponents
{
    public class _RelatedCoursesPartial : ViewComponent
    {
        private readonly CustomHttpClient _customHttpClient;
        public _RelatedCoursesPartial(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = Convert.ToInt32(Encoding.UTF8.GetString(HttpContext.Session.Get("userId")));
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Widget", Action= "CourseListByUser",QueryString=$"id={userId}" });

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<WidgetCourseByUserDto>>(jsonData);
                return View(values);
            }
            return View();
        }

    }
}
