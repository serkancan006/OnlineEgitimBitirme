using System.ComponentModel.DataAnnotations;

namespace OnlineEgitimClient.Dtos.AppUserDto
{
    public class LoginAppUserDto
    {
        [Required(ErrorMessage = "Kullanıcı adınızı giriniz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre giriniz")]
        public string Password { get; set; }
    }
}
