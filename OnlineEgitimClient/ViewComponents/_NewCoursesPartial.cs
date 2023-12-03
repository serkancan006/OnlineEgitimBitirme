using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Service;

namespace OnlineEgitimClient.ViewComponents
{
    public class _NewCoursesPartial : ViewComponent
    {
        private readonly CustomHttpClient _customHttpClient;
        public _NewCoursesPartial(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Widget" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ListCourseDto>>(jsonData);
                return View(values);
            }
            return View();
        }

    }
}
