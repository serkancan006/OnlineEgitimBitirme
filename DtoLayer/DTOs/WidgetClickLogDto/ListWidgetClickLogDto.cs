using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.WidgetClickLogDto
{
    public class ListWidgetClickLogDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }

        public int AppUserID { get; set; }
        public int CourseID { get; set; }
        public int CourseViewCount { get; set; }
    }
}
