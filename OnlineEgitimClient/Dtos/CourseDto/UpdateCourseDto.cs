﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineEgitimClient.Dtos.CourseDto
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Duration { get; set; } // eğitim süresi
        [JsonIgnore]
        public IFormFile? ImageUrl { get; set; }
        //public IFormFile? ImageFile { get; set; }
        public int? SubjectCount { get; set; }
        public string? Level { get; set; }
        public string? Language { get; set; }

        //public int CourseLike { get; set; }
        //public int CourseDisLike { get; set; }
        //public int CoursePuan { get; set; }
        //public int CourseViewCountLog { get; set; }

        public int LocationID { get; set; }
        public int AppUserID { get; set; }
    }
}
