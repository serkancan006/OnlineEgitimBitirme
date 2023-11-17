using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.CourseVideoFileDto
{
    public class UpdateCourseVideoFileDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileDisplayName { get; set; }
        public int CourseID { get; set; }
        //public Course Course { get; set; }
    }
}
