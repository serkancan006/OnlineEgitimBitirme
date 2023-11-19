using System.ComponentModel.DataAnnotations;

namespace OnlineEgitimClient.Dtos.AppUserDto
{
    public class LoginAppUserDto
    {
        [Required(ErrorMessage = "Kullanıcı adınızı yada Emailinizi giriniz")]
        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Şifre giriniz")]
        public string Password { get; set; }
    }
}
