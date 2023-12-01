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

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserWithRole(RegisterAppUserDto registerAppUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingUser = await _userManager.FindByEmailAsync(registerAppUserDto.Mail);
            if (existingUser != null)
            {
                // Kullanıcı bulundu, rol atama kontrolü yapılacak
                if (await _userManager.IsInRoleAsync(existingUser, "Instructor"))
                {
                    // Kullanıcı belirtilen role zaten sahip
                    return BadRequest("Bu kullanıcı zaten belirtilen role sahip.");
                }
                else
                {
                    // Kullanıcıya belirtilen rol atanacak
                    await _userManager.AddToRoleAsync(existingUser, "Instructor");
                    return Ok("Kullanıcıya rol başarıyla eklendi.");
                }
            }
            else
            {
                // Kullanıcı bulunamadı, yeni kullanıcı oluşturulacak ve role atanacak
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
                {
                    // Yeni kullanıcı oluşturuldu, belirtilen rol atanacak
                    await _userManager.AddToRoleAsync(appUser, "Instructor");
                    return Ok("Kullanıcı başarıyla oluşturuldu ve role eklendi.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
        }

    }
}
