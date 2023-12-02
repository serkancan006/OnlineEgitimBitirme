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


        //[HttpGet("[action]")]
        //public IActionResult CourseListByUser(int id)
        //{
        //    var values = _widgetClickLogService.TGetListTrueStatus().Where(x => x.AppUserID == id);
        //    foreach (var item in values)
        //    {
        //        item.ImageUrl = "https://" + _configuration["BaseUrl"] + item.ImageUrl;
        //    }
        //    return Ok(values);
        //}

    }
}
