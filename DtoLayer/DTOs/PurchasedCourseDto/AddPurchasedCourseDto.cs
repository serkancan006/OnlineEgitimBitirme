using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.PurchasedCourseDto
{
    public class AddPurchasedCourseDto
    {
        public int AppUserID { get; set; }
        public int CourseID { get; set; }
    }
}
