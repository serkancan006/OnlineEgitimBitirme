using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineEgitimClient.Dtos.CourseDto
{
    public class AddCourseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Duration { get; set; } // eğitim süresi
        public IFormFile? ImageUrl { get; set; }
        public int? SubjectCount { get; set; }
        public string? Level { get; set; }
        public string? Language { get; set; }
        public int AppUserID { get; set; } //InstructorID
        //public int CourseLike { get; set; }
        //public int CourseDisLike { get; set; }
        //public int CoursePuan { get; set; }
        //public int CourseViewCountLog { get; set; }
    }
}
