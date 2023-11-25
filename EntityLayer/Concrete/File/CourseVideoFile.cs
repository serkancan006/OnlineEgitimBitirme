using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.File
{
    public class CourseVideoFile : File
    {
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
