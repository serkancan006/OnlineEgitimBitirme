using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.AppUserDto;
using EntityLayer.Concrete;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterAppUserDto registerAppUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appUser = new AppUser()
            {
                Name = registerAppUserDto.Name,
                Email = registerAppUserDto.Mail,
                Surname = registerAppUserDto.Surname,
                UserName = registerAppUserDto.UserName,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            var result = await _userManager.CreateAsync(appUser, registerAppUserDto.Password);

            if (result.Succeeded)
                return Ok("Kullanıcı başarıyla eklendi.");
            else
                return BadRequest(result.Errors);

        }
    }
}
