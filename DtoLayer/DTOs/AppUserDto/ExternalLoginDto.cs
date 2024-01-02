using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.AppUserDto
{
    public class ExternalLoginDto
    {
        public string userEmail { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string provider { get; set; }
        public string accessToken { get; set; }
    }
}
