using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ICreateTokenService _createTokenService;

        public TokenController(ICreateTokenService createTokenService)
        {
            _createTokenService = createTokenService;
        }

        [HttpGet("[action]")]
        public IActionResult TokenOlustur()
        {
            return Ok(_createTokenService.TokenCreate());
        }

        [HttpGet("[action]")]
        public IActionResult AdminTokenOlustur()
        {
            return Ok(_createTokenService.TokenCreateAdmin());
        }

        [HttpGet("[action]")]
        [Authorize]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Test()
        {
            return Ok("Hoşgeldiniz");
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin,Instructor,Visitor")]
        public IActionResult TestAdmin()
        {
            return Ok("Token başarılı bir şekilde giriş yaptı");
        }
    }
}
