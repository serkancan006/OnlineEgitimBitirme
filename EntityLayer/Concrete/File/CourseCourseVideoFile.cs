using EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.File
{
    public class CourseCourseVideoFile : BaseEntity
    {
        public int CourseVideoImageFile { get; set; }
        public CourseImageFile CourseImageFile { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
