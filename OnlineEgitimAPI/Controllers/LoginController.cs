using DtoLayer.DTOs.AppUserDto;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, false, false);

                if (result.Succeeded)
                    return Ok(new { message = "Kullanıcı adı ve şifre dogru.", status = true });
                else
                    return BadRequest(new { message = "Kullanıcı adı veya şifre hatalı.", status = false, error = result });
            }

            return BadRequest(new { message = "Gönderilen veri hatalı.", status = false });
        }
    }
}