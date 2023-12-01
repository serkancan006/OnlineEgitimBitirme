using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.CourseDto;
using DtoLayer.DTOs.CourseVideoFileDto;
using EntityLayer.Concrete;
using EntityLayer.Concrete.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.RegularExpressions;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _CourseService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public CourseController(ICourseService courseService, IMapper mapper, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _CourseService = courseService;
            _mapper = mapper;
            _environment = environment;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult CourseList()
        {
            var values = _CourseService.TGetList();
            foreach (var item in values)
            {
                item.ImageUrl = "https://" + _configuration["BaseUrl"] + item.ImageUrl;
            }
            return Ok(values);
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromForm]AddCourseDto addCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var rootPath = _environment.WebRootPath;
            var path = "images/CourseImage/";
            var fullPath = Path.Combine(rootPath, path);
            var fileName = GetUniqueFileName(addCourseDto.ImageUrl.FileName);
            var filePath = Path.Combine(fullPath, fileName);
            var databasePath = path + fileName;
            // Eğer belirtilen dizin yoksa oluştur
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            try
            {
                if (addCourseDto.ImageUrl == null || addCourseDto.ImageUrl.Length <= 0)
                    return BadRequest("Geçersiz dosya");


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await addCourseDto.ImageUrl.CopyToAsync(stream);
                }

                var values = _mapper.Map<Course>(addCourseDto);
                values.ImageUrl = databasePath;

                _CourseService.TAdd(values);

                //return Ok($"Dosya Adı: {file.FileName}, belirtilen klasöre yüklendi: {path}");
                return Ok("Kurs Başarıyla Eklendi");
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
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<Course>(updateCourseDto);
            _CourseService.TUpdate(values);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            var values = _CourseService.TGetByID(id);
            values.ImageUrl = "https://"+_configuration["BaseUrl"] + values.ImageUrl;
            return Ok(values);
        }
        [HttpGet("[action]/{id}")]
        public IActionResult GetCourseByUser(int id)
        {
            var values = _CourseService.TGetByID(id);
            values.CourseViewCountLog += 1;
            Course model = _mapper.Map<Course>(values);
            _CourseService.TUpdate(model);
            values.ImageUrl = "https://" + _configuration["BaseUrl"] + values.ImageUrl;
            return Ok(values);
        }
        [HttpGet("[action]")]
        public IActionResult CourseListByStatus()
        {
            var values = _CourseService.TGetListTrueStatus();
            foreach (var item in values)
            {
                item.ImageUrl = "https://"+_configuration["BaseUrl"] + item.ImageUrl;
            }
            return Ok(values);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult CourseListByInstructor(int id)
        {
            var values = _CourseService.TGetListByInstructor(id);
            foreach (var item in values)
            {
                item.ImageUrl = "https://" + _configuration["BaseUrl"] + item.ImageUrl;
            }
            return Ok(values);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string fileExtension = Path.GetExtension(fileName);
            string pureFileName = Path.GetFileNameWithoutExtension(fileName);

            pureFileName = Regex.Replace(pureFileName, "[^a-zA-Z0-9]", "-");

            string newFileName = pureFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension;
            return newFileName;
        }
    }
}
