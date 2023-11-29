﻿using BusinessLayer.Abstract;
using DtoLayer.DTOs.AppUserDto;
using DtoLayer.DTOs.TokenDto;
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
        private readonly ICreateTokenService _createTokenService;
        private readonly UserManager<AppUser> _userManager;
        public LoginController(SignInManager<AppUser> signInManager, ICreateTokenService createTokenService, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _createTokenService = createTokenService;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginUserDto.UsernameOrEmail) ?? await _userManager.FindByEmailAsync(loginUserDto.UsernameOrEmail);

                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginUserDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (user != null)
                        {
                            var userRoles = await _userManager.GetRolesAsync(user);
                            //if (userRoles.Any(r => r == "Admin"))
                            if (userRoles.Contains("Admin"))
                            {
                                // Kullanıcı Admin içeriği içeriyorsa 
                                var value = _createTokenService.TokenCreateAdmin();
                                return Ok(new { message = "Admini girişi başarılı.", value, userId = user.Id });
                            }
                            else if (userRoles.Contains("Instructor"))
                            {
                                // Kullanıcı Instructor içeriği içeriyorsa 
                                var value = _createTokenService.TokenCreateInstructor();
                                return Ok(new { message = "Eğitmen girişi başarılı.", value, userId = user.Id });
                            }
                            else
                            {
                                var value = _createTokenService.TokenCreate();
                                return Ok(new { message = "Kullanıcı girişi başarılı.", value, userId = user.Id });
                            }
                        }
                        //return Ok(value);
                    }
                    else
                        return BadRequest(new { message = "Kullanıcı adı veya şifre hatalı.", status = false, error = result });
                }
            }
            return BadRequest(new { message = "Gönderilen veriler hatalı.", status = false });
        }
    }
}