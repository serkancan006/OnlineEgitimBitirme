using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.ExternalService;
using DtoLayer.DTOs.CourseVideoFileDto;
using EntityLayer.Concrete.File;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseVideoFileController : ControllerBase
    {
        private readonly ICourseVideoFileService _courseVideoFileService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserCourseAccessService _userCourseAccessService;
        private readonly IFileOperationsService _fileOperationsService;

        public CourseVideoFileController(ICourseVideoFileService courseVideoFileService, IMapper mapper, UserManager<AppUser> userManager, IUserCourseAccessService userCourseAccessService, IFileOperationsService fileOperationsService)
        {
            _courseVideoFileService = courseVideoFileService;
            _mapper = mapper;
            _userManager = userManager;
            _userCourseAccessService = userCourseAccessService;
            _fileOperationsService = fileOperationsService;
        }


        //https://localhost:7064/videos/CourseVideo/deneme_20231116183017533.mp4  adresine kaydedilir
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> AddCourseVideoFile([FromForm] IFormFile file, int id)
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return BadRequest("Geçersiz dosya");

                var (fileName, databasePath) = await _fileOperationsService.SaveFileAsync(file, "videos/CourseVideo/");

                var fileModel = new AddCourseVideoFileDto
                {
                    FileName = fileName,
                    FilePath = databasePath,
                    FileDisplayName = Path.GetFileNameWithoutExtension(file.FileName),
                    CourseID = id
                };

                var values = _mapper.Map<CourseVideoFile>(fileModel);
                _courseVideoFileService.TAdd(values);


                return Ok("Dosya başarıyla yüklendi");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Dosya yükleme hatası: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourseVideoFile(int id)
        {
            try
            {
                var values = _courseVideoFileService.TGetByID(id);
                var deleted = _fileOperationsService.DeleteFile(values.FilePath);

                if (deleted)
                {
                    _courseVideoFileService.TDelete(values);
                    return Ok("Dosya Başarıyla Silindi");
                }

                return NotFound("Belirtilen dosya bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Dosya silme hatası: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("{id}")]
        public IActionResult GetCourseVideoFile(int id)
        {
            var values = _courseVideoFileService.CourseGetVideoFiles(id);
            var mappedValues = _mapper.Map<List<ListCourseVideoFileDto>>(values);
            foreach (ListCourseVideoFileDto file in mappedValues)
            {
                file.FilePath = _fileOperationsService.GetFileConvertUrl(file.FilePath);
            }
            return Ok(mappedValues);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCourseVideoFileWithUser(int courseId, string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
                bool hasAccess = _userCourseAccessService.HasAccessToCourse(user.Id, courseId);
                if (hasAccess)
                {
                    var values = _courseVideoFileService.CourseGetVideoFiles(courseId);
                    var mappedValues = _mapper.Map<List<ListCourseVideoFileDto>>(values);
                    foreach (ListCourseVideoFileDto file in mappedValues)
                    {
                        file.FilePath = _fileOperationsService.GetFileConvertUrl(file.FilePath);
                    }
                    return Ok(mappedValues);
                }
                else
                {
                    return Unauthorized("Bu kursa erişim izniniz yok.");
                }
            }
            return NotFound("Kullanıcı bulunamadı.");
        }

    }
}
