using AutoMapper;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly ICourseService _CourseService;
        private readonly IWidgetClickLogService _widgetClickLogService;
        private readonly IPurchasedCourseService _purchasedCourseService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public StatisticController(ICourseService courseService, IMapper mapper, IWebHostEnvironment environment, IConfiguration configuration, IWidgetClickLogService widgetClickLogService, IPurchasedCourseService purchasedCourseService)
        {
            _CourseService = courseService;
            _mapper = mapper;
            _environment = environment;
            _configuration = configuration;
            _widgetClickLogService = widgetClickLogService;
            _purchasedCourseService = purchasedCourseService;
        }

        //Eğitmenin Toplam Eğitim Sayısı
        [HttpGet("[action]/{id}")]
        public IActionResult InstructorCourseCount(int id)
        {
            var values = _CourseService.TGetListByInstructor(id).Count();
            return Ok(values);
        }
        //Eğitmenin Satılan Eğitimlerinin Sayısı
        [HttpGet("[action]/{id}")]
        public IActionResult InstructorPurchasedCourseCount(int id)
        {
            var values = _purchasedCourseService.TCourseListIncludewhereInstructor(id).Count();
            return Ok(values);
        }
        //Eğitmenin en çok görüntülenen eğitimleri
        [HttpGet("[action]/{id}")]
        public IActionResult InstructorMostViewCourse(int id)
        {
            var values = _CourseService.TGetListByInstructor(id).OrderByDescending(x => x.CourseViewCountLog).Take(5).ToList();
            foreach (var item in values)
            {
                item.ImageUrl = "https://" + _configuration["BaseUrl"] + item.ImageUrl;
            }
            return Ok(values);
        }
        //Eğitmenini en Çok satılan eğitimleri ve satış sayısı
        [HttpGet("[action]/{id}")]
        public IActionResult InstructorMostPurchasedCourses(int id)
        {
            var coursesWithPurchases = _purchasedCourseService.TCourseListIncludewhereInstructor(id)
                .GroupBy(course => new { course.CourseID, course.Course.Title, course.Course.ImageUrl, course.Course.Price }) // Kurs adını ve kimliğini gruplayın
                .Select(group => new
                {
                    CourseID = group.Key.CourseID,
                    Title = group.Key.Title,
                    ImageUrl = "https://" + _configuration["BaseUrl"] + group.Key.ImageUrl,
                    Price = group.Key.Price,
                    PurchaseCount = group.Count()
                })
                .OrderByDescending(x => x.PurchaseCount)
                .Take(5) // İstenilen en fazla eğitim sayısı
                .ToList();

            return Ok(coursesWithPurchases);
        }
        [HttpGet("[action]/{id}")]
        public IActionResult InstructorTotalViewCount(int id)
        {
            var courses = _CourseService.TGetListByInstructor(id); // Eğitmenin tüm eğitimleri

            int totalViewCount = courses.Sum(course => course.CourseViewCountLog); // Tüm eğitimlerin toplam görüntülenme sayısı

            return Ok(totalViewCount);
        }
        //**********************************************ADMİN DASHBOARD KISMI *****************************************************************
        [HttpGet("[action]")]
        public IActionResult CourseCount()
        {
            var values = _CourseService.TGetList().Count();
            return Ok(values);
        }
        //Eğitmenin Satılan Eğitimlerinin Sayısı
        [HttpGet("[action]")]
        public IActionResult PurchasedCourseCount()
        {
            var values = _purchasedCourseService.TGetList().Count();
            return Ok(values);
        }
        //Eğitmenin en çok görüntülenen eğitimleri
        [HttpGet("[action]")]
        public IActionResult MostViewCourse()
        {
            var values = _CourseService.TGetList().OrderByDescending(x => x.CourseViewCountLog).Take(5).ToList();
            foreach (var item in values)
            {
                item.ImageUrl = "https://" + _configuration["BaseUrl"] + item.ImageUrl;
            }
            return Ok(values);
        }
        //Eğitmenini en Çok satılan eğitimleri ve satış sayısı
        [HttpGet("[action]")]
        public IActionResult MostPurchasedCourses()
        {
            var coursesWithPurchases = _purchasedCourseService.TCourseListInclude()
                .GroupBy(course => new { course.CourseID, course.Course.Title, course.Course.ImageUrl, course.Course.Price }) // Kurs adını ve kimliğini gruplayın
                .Select(group => new
                {
                    CourseID = group.Key.CourseID,
                    Title = group.Key.Title,
                    ImageUrl = "https://" + _configuration["BaseUrl"] + group.Key.ImageUrl,
                    Price = group.Key.Price,
                    PurchaseCount = group.Count()
                })
                .OrderByDescending(x => x.PurchaseCount)
                .Take(5) // İstenilen en fazla eğitim sayısı
                .ToList();

            return Ok(coursesWithPurchases);
        }
        [HttpGet("[action]")]
        public IActionResult TotalViewCount()
        {
            var courses = _CourseService.TGetList(); // Eğitmenin tüm eğitimleri

            int totalViewCount = courses.Sum(course => course.CourseViewCountLog); // Tüm eğitimlerin toplam görüntülenme sayısı

            return Ok(totalViewCount);
        }
    }
}
