using AutoMapper;
using EntityLayer.Concrete;
using Iyzipay.Model.V2.Subscription;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Dtos.WidgetClickLogDto;
using OnlineEgitimClient.Service;
using System.Text;

namespace OnlineEgitimClient.Controllers
{
    public class CourseController : Controller
    {
        private readonly CustomHttpClient _customHttpClient;
        public CourseController(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }


        public async Task<IActionResult> Index(string searchString, int pageNumber = 1, int pageSize = 6)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course", Action = "CourseListByStatus" });
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var allCourses = JsonConvert.DeserializeObject<List<ListCourseDto>>(jsonData);

                // Eğer bir arama string'i belirtildiyse, kursları filtrele
                if (!string.IsNullOrEmpty(searchString))
                {
                    allCourses = allCourses?.Where(course => course.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (allCourses != null)
                {
                    var totalItems = allCourses.Count();
                    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                    allCourses = allCourses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    var model = new PaginatedList<ListCourseDto>(allCourses, totalItems, pageNumber, pageSize);

                    ViewBag.CurrentFilter = searchString;
                    return View(model);
                }
                else
                {
                    // allCourses null ise ya da bir hata oluştuysa, boş bir model veya hata sayfası döndürebiliriz
                    var emptyModel = new PaginatedList<ListCourseDto>(new List<ListCourseDto>(), 0, pageNumber, pageSize);
                    ViewBag.CurrentFilter = searchString;
                    return View(emptyModel);
                }
            }
            return View();
        }



        public async Task<IActionResult> CourseDetails(int id)
        {
            var userId = HttpContext.Session.Get("userId");
            if (userId != null)
            {
                AddWidgetClickLogDto model = new AddWidgetClickLogDto();
                model.CourseID = id;
                model.AppUserID = Convert.ToInt32(Encoding.UTF8.GetString(userId));
                await _customHttpClient.Post(new() { Controller = "WidgetClickLog" }, model);
            }

            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course", Action = "GetCourseByUser" }, id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ListCourseDto>(jsonData);
                return View(values);
            }

            return View();
        }


    }
}
