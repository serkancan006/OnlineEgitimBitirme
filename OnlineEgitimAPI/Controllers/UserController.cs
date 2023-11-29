using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult UserList()
        {
            return Ok();
        }
    }
}
