﻿using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.WidgetClickLogDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetClickLogController : ControllerBase
    {
        private readonly IWidgetClickLogService _WidgetClickLogService;
        private readonly IMapper _mapper;
        public WidgetClickLogController(IWidgetClickLogService WidgetClickLogService, IMapper mapper)
        {
            _WidgetClickLogService = WidgetClickLogService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult WidgetClickLogList()
        {
            var values = _WidgetClickLogService.TGetList();
            return Ok(values);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddWidgetClickLog(AddWidgetClickLogDto addWidgetClickLogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<WidgetClickLog>(addWidgetClickLogDto);
            var haswidget = _WidgetClickLogService.TGetByAppUserIDAndCourseID(addWidgetClickLogDto.AppUserID, addWidgetClickLogDto.CourseID);
            if (haswidget)
                return Ok();
            else
            {
                values.CourseViewCount = 1;
                _WidgetClickLogService.TAdd(values);
                return Ok();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteWidgetClickLog(int id)
        {
            var values = _WidgetClickLogService.TGetByID(id);
            _WidgetClickLogService.TDelete(values);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateWidgetClickLog(UpdateWidgetClickLogDto updateWidgetClickLogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<WidgetClickLog>(updateWidgetClickLogDto);
            _WidgetClickLogService.TUpdate(values);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetWidgetClickLog(int id)
        {
            var values = _WidgetClickLogService.TGetByID(id);
            return Ok(values);
        }
    }
}
