using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.AppUserDto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Kullanıcı adı veya Mail Giriniz")]
        public string UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Şifre Giriniz")]
        public string Password { get; set; }
    }
}
