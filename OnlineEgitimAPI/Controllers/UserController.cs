using DtoLayer.DTOs.AppUserDto;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> InstructorList()
        {
            var usersInRole = (await _userManager.GetUsersInRoleAsync("Instructor")).Take(10);

            return Ok(usersInRole);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            AppUserDto value = new AppUserDto()
            {
                Mail = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName
            };
            return Ok(value);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetUser(AppUserDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            user.Name = model.Name;
            user.Surname = model.Surname;
            await _userManager.UpdateAsync(user);
            return Ok("Profil güncellemesi başarılı");
        }

    }
}
