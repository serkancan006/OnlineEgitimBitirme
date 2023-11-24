using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using DtoLayer.DTOs.PurchasedCourseDto;
using EntityLayer.Concrete;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedCourseController : ControllerBase
    {
        private readonly IPurchasedCourseService _PurchasedCourseService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public PurchasedCourseController(IPurchasedCourseService PurchasedCourseService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _PurchasedCourseService = PurchasedCourseService;
            _mapper = mapper;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult PurchasedCourseList()
        {
            var values = _PurchasedCourseService.TGetList();
            return Ok(values);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddPurchasedCourse(AddPurchasedCourseDto addPurchasedCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<PurchasedCourse>(addPurchasedCourseDto);
            _PurchasedCourseService.TAdd(values);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeletePurchasedCourse(int id)
        {
            var values = _PurchasedCourseService.TGetByID(id);
            _PurchasedCourseService.TDelete(values);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdatePurchasedCourse(UpdatePurchasedCourseDto updatePurchasedCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<PurchasedCourse>(updatePurchasedCourseDto);
            _PurchasedCourseService.TUpdate(values);
            return Ok();
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPurchasedCourse(int id)
        {
            var values = _PurchasedCourseService.TGetByID(id);
            return Ok(values);
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> PurchasedCourseList(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var values = _PurchasedCourseService.TCourseListInclude().Where(x => x.AppUserID == user?.Id).Select(x => new
            {
                x.CourseID,
                x.Course.ImageUrl,
                x.Course.Title,
                x.Course.Description
            });
            return Ok(values);
        }
    }
}
