using EntityLayer.Concrete.Common;
using EntityLayer.Concrete.File;
using EntityLayer.Concrete.identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Course : BaseEntity
	{
        public string Title { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
        public string Duration { get; set; } // eğitim süresi
        public string ImageUrl { get; set; }
		public int SubjectCount { get; set; }
		public string Level { get; set; }
		public string Language { get; set; }

        public int CourseLike { get; set; }
        public int CourseDisLike { get; set; }
        public int CoursePuan { get; set; }
        public int CourseViewCountLog { get; set; }

		public int LocationID { get; set; }
		public Location Location { get; set; }
		public int AppUserID { get; set; }
		public AppUser AppUser { get; set; }

		public ICollection<PurchasedCourse> PurchasedCourses { get; set; }
		public ICollection<WidgetClickLog> WidgetClickLogs { get; set; }
		public ICollection<CourseVideoFile> CourseVideoFiles { get; set; }
	}
}

