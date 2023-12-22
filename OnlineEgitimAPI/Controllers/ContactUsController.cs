using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DTOs.ContactUsDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _ContactUsService;
        private readonly IMapper _mapper;
        public ContactUsController(IContactUsService ContactUsService, IMapper mapper)
        {
            _ContactUsService = ContactUsService;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ContactUsList()
        {
            var values = _ContactUsService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddContactUs(AddContactUsDto addContactUsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<ContactUs>(addContactUsDto);
            _ContactUsService.TAdd(values);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteContactUs(int id)
        {
            var values = _ContactUsService.TGetByID(id);
            _ContactUsService.TDelete(values);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateContactUs(UpdateContactUsDto updateContactUsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<ContactUs>(updateContactUsDto);
            _ContactUsService.TUpdate(values);
            return Ok("Başarıyla GÜncellendi");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetContactUs(int id)
        {
            var values = _ContactUsService.TGetByID(id);
            return Ok(values);
        }
    }
}
