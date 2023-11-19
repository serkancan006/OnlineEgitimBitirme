using AutoMapper;
using BusinessLayer.Abstract;
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
    public class CourseVideoFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ICourseVideoFileService _courseVideoFileService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CourseVideoFileController(IWebHostEnvironment environment, ICourseVideoFileService courseVideoFileService, IMapper mapper, IConfiguration configuration)
        {
            _environment = environment;
            _courseVideoFileService = courseVideoFileService;
            _mapper = mapper;
            _configuration = configuration;
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


        //https://localhost:7064/videos/CourseVideo/deneme_20231116183017533.mp4  adresine kaydedilir
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> AddCourseVideoFile([FromForm] IFormFile file, int id)
        {
            var rootPath = _environment.WebRootPath;
            var path = "videos/CourseVideo/";
            var fullPath = Path.Combine(rootPath, path);
            var fileName = GetUniqueFileName(file.FileName);
            var filePath = Path.Combine(fullPath, fileName);
            var databasePath = path + fileName;

            // Eğer belirtilen dizin yoksa oluştur
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            try
            {
                if (file == null || file.Length <= 0)
                    return BadRequest("Geçersiz dosya");


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileModel = new AddCourseVideoFileDto
                {
                    FileName = fileName,
                    FilePath = databasePath,
                    FileDisplayName = Path.GetFileNameWithoutExtension(file.FileName),
                    CourseID = id
                };

                var values = _mapper.Map<CourseVideoFile>(fileModel);
                _courseVideoFileService.TAdd(values);

                //return Ok($"Dosya Adı: {file.FileName}, belirtilen klasöre yüklendi: {path}");
                return Ok("Dosya başarıyla yüklendi");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Dosya yükleme hatası: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourseVideoFile(int id)
        {
            try
            {
                var values = _courseVideoFileService.TGetByID(id);
                var filePath = Path.Combine(_environment.WebRootPath, values.FilePath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    _courseVideoFileService.TDelete(values);
                    //return Ok($"Dosya '{values.FileDisplayName}' başarıyla silindi.");
                    return Ok("Dosya Başarıyla Silindi");
                }

                return NotFound("Belirtilen dosya bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Dosya silme hatası: {ex.Message}");
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetCourseVideoFile(int id)
        {
            var values = _courseVideoFileService.CourseGetVideoFiles(id);
            var mappedValues = _mapper.Map<List<ListCourseVideoFileDto>>(values);
            foreach (ListCourseVideoFileDto file in mappedValues)
            {
                file.FilePath = _configuration["BaseUrl"] + file.FilePath;
            }
            return Ok(mappedValues);
        }

    }
}
