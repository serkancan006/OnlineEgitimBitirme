using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.AboutDto
{
    public class UpdateAboutDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image1 { get; set; }
        public string Title2 { get; set; }
        public string Description2 { get; set; }
        public string Image2 { get; set; }
        public string SubTitle { get; set; }
        public string SubDesc { get; set; }
        public string SubTitle2 { get; set; }
        public string SubDesc2 { get; set; }
        public string SubTitle3 { get; set; }
        public string SubDesc3 { get; set; }
    }
}
