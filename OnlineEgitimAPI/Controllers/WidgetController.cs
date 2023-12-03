using AutoMapper;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        private readonly ICourseService _CourseService;
        private readonly IWidgetClickLogService _widgetClickLogService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public WidgetController(ICourseService courseService, IMapper mapper, IWebHostEnvironment environment, IConfiguration configuration, IWidgetClickLogService widgetClickLogService)
        {
            _CourseService = courseService;
            _mapper = mapper;
            _environment = environment;
            _configuration = configuration;
            _widgetClickLogService = widgetClickLogService;
        }


        [HttpGet]
        public IActionResult CourseList()
        {
            var values = _CourseService.TGetListTrueStatus().OrderByDescending(x => x.CreatedDate);
            foreach (var item in values)
            {
                item.ImageUrl = "https://" + _configuration["BaseUrl"] + item.ImageUrl;
            }
            return Ok(values);
        }


        [HttpGet("[action]")]
        public IActionResult CourseListByUser(int id)
        {
            //var values = _widgetClickLogService.TWidgetListIncludeTrueStatus().Where(x => x.AppUserID == id).Select(y => new
            //{
            //    y.AppUserID,
            //    y.CourseID,
            //    ImageUrl = "https://" + _configuration["BaseUrl"] + y.Course.ImageUrl,
            //    y.Course.Title,
            //    y.Course.Price,
            //    y.Course.Level,
            //    y.Course.Language,
            //    y.Course.CourseViewCountLog
            //});

            //var values = _widgetClickLogService
            //    .TWidgetListIncludeTrueStatus()
            //    .Where(x => x.AppUserID == id)
            //    .GroupBy(y => y.CourseID)
            //    .Select(group => new
            //    {
            //        CourseID = group.Key,
            //        Count = group.Count(),
            //        CourseDetails = group.First().Course // Sadece kurs detaylarına erişim
            //    })
            //    .OrderByDescending(courseGroup => courseGroup.Count)
            //    .Take(3)
            //    .Select(courseGroup => new
            //    {
            //        courseGroup.CourseID,
            //        courseGroup.CourseDetails.AppUserID,
            //        courseGroup.CourseDetails.Title,
            //        courseGroup.CourseDetails.Price,
            //        courseGroup.CourseDetails.Level,
            //        courseGroup.CourseDetails.Language
            //        // İhtiyaç duyulan diğer özellikler
            //    });

            var values = _widgetClickLogService.TWidgetListIncludeTrueStatus().Where(x => x.AppUserID == id).Select(y => new
            {
                y.AppUserID,
                y.CourseID,
                ImageUrl = "https://" + _configuration["BaseUrl"] + y.Course.ImageUrl,
                y.Course.Title,
                y.Course.Price,
                y.Course.Level,
                y.Course.Language,
                y.Course.CourseViewCountLog
            }).OrderByDescending(x => x.CourseViewCountLog).Take(3);

            return Ok(values);
        }

    }
}
