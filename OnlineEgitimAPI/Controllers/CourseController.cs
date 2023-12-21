using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.ExternalService;
using DtoLayer.DTOs.CourseDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _CourseService;
        private readonly IMapper _mapper;
        private readonly IFileOperationsService _fileOperationsService;

        public CourseController(ICourseService courseService, IMapper mapper, IFileOperationsService fileOperationsService)
        {
            _CourseService = courseService;
            _mapper = mapper;
            _fileOperationsService = fileOperationsService;
        }

        [HttpGet]
        public IActionResult CourseList()
        {
            var values = _CourseService.TGetList();
            foreach (var item in values)
            {
                item.ImageUrl = _fileOperationsService.GetFileConvertUrl(item.ImageUrl);
            }
            return Ok(values);
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromForm] AddCourseDto addCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                if (addCourseDto.ImageUrl == null || addCourseDto.ImageUrl.Length <= 0)
                    return BadRequest("Geçersiz dosya");

                var values = _mapper.Map<Course>(addCourseDto);
                var (fileName, databasePath) = await _fileOperationsService.SaveFileAsync(addCourseDto.ImageUrl, "images/CourseImage/");
                values.ImageUrl = databasePath;

                _CourseService.TAdd(values);

                return Ok("Kurs Başarıyla Eklendi");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Dosya yükleme hatası: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromForm] UpdateCourseDto updateCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                if (updateCourseDto.ImageUrl == null || updateCourseDto.ImageUrl.Length <= 0)
                {
                    var course = _CourseService.TGetByID(updateCourseDto.Id);
                    var values = _mapper.Map<Course>(updateCourseDto);
                    values.ImageUrl = course.ImageUrl;
                    _CourseService.TUpdate(values);
                    return Ok("Kurs Başarıyla Güncellendi Resimsiz");
                }
                else
                {
                    var values = _mapper.Map<Course>(updateCourseDto);
                    _fileOperationsService.DeleteFile(values.ImageUrl);
                    var (fileName, databasePath) = await _fileOperationsService.SaveFileAsync(updateCourseDto.ImageUrl, "images/CourseImage/");
                    values.ImageUrl = databasePath;
                    _CourseService.TUpdate(values);

                    return Ok("Kurs Başarıyla Güncellendi Resimli");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Dosya yükleme hatası: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var values = _CourseService.TGetByID(id);
            _CourseService.TDelete(values);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            var values = _CourseService.TGetByID(id);
            values.ImageUrl = _fileOperationsService.GetFileConvertUrl(values.ImageUrl);
            return Ok(values);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetCourseByUser(int id)
        {
            var values = _CourseService.TGetByID(id);
            values.CourseViewCountLog += 1;
            Course model = _mapper.Map<Course>(values);
            _CourseService.TUpdate(model);
            values.ImageUrl = _fileOperationsService.GetFileConvertUrl(values.ImageUrl);
            return Ok(values);
        }

        [HttpGet("[action]")]
        public IActionResult CourseListByStatus()
        {
            var values = _CourseService.TGetListTrueStatus();
            foreach (var item in values)
            {
                item.ImageUrl = _fileOperationsService.GetFileConvertUrl(item.ImageUrl);
            }
            return Ok(values);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult CourseListByInstructor(int id)
        {
            var values = _CourseService.TGetListByInstructor(id);
            foreach (var item in values)
            {
                item.ImageUrl = _fileOperationsService.GetFileConvertUrl(item.ImageUrl);
            }
            return Ok(values);
        }

        [HttpGet("include")]
        public IActionResult CourseListInclude()
        {
            var values = _CourseService.TGetListInclude().Select(x => new
            {
                ImageUrl = _fileOperationsService.GetFileConvertUrl(x.ImageUrl),
                UserName = x.AppUser.UserName,
                AppUserID = x.AppUserID
            }).ToList();

            return Ok(values);
        }


    }
}
