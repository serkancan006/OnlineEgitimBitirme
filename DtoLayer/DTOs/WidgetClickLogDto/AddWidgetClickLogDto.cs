using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.WidgetClickLogDto
{
    public class AddWidgetClickLogDto
    {
        public int AppUserID { get; set; }
        public int CourseID { get; set; }
        public int CourseViewCount { get; set; }
    }
}
