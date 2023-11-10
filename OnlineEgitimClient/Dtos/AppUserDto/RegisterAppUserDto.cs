using System.ComponentModel.DataAnnotations;

namespace OnlineEgitimClient.Dtos.AppUserDto
{
    public class RegisterAppUserDto
    {
        [Required(ErrorMessage = "Adınızı giriniz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyadınızı giriniz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Kullanıcı Adınızı giriniz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mail Adresinizi giriniz")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Şifre giriniz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre Tekrar giriniz.")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
