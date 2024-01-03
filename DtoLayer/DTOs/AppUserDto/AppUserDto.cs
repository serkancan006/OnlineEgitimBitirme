using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.AppUserDto
{
    public class AppUserDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? UserName { get; set; }
        public string? Mail { get; set; }
    }
}
