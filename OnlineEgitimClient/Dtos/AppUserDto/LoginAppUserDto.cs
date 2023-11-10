using System.ComponentModel.DataAnnotations;

namespace OnlineEgitimClient.Dtos.AppUserDto
{
    public class LoginAppUserDto
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        [Required(ErrorMessage = "Şifre giriniz")]
        public string Password { get; set; }
    }
}
