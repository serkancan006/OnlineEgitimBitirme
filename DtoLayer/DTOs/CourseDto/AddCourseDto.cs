using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.CourseDto
{
    public class AddCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string Duration { get; set; } // eğitim süresi
        public string ImageUrl { get; set; }
        public int SubjectCount { get; set; }
        public string Level { get; set; }
        public string Language { get; set; }

        public int CourseLike { get; set; }
        public int CourseDisLike { get; set; }
        public int CoursePuan { get; set; }
        public int CourseViewCountLog { get; set; }
    }
}
