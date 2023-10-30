using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetClickLogController : ControllerBase
    {
        private readonly IWidgetClickLogService _widgetClickLogService;

        public WidgetClickLogController(IWidgetClickLogService widgetClickLogService)
        {
            _widgetClickLogService = widgetClickLogService;
        }
        [HttpGet]
        public IActionResult WidgetClickLogList()
        {
            var values = _widgetClickLogService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddWidgetClickLog(WidgetClickLog widgetClickLog)
        {
            _widgetClickLogService.TAdd(widgetClickLog);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteWidgetClickLog(int id)
        {
            var values = _widgetClickLogService.TGetByID(id);
            _widgetClickLogService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateWidgetClickLog(WidgetClickLog widgetClickLog)
        {
            _widgetClickLogService.TUpdate(widgetClickLog);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetWidgetClickLog(int id)
        {
            var values = _widgetClickLogService.TGetByID(id);
            return Ok(values);
        }
    }
}
