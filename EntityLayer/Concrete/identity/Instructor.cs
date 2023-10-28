using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.identity
{
	public class Instructor : AppUser
	{
		public string InstructorName { get; set; }
		public string InstructorSurname { get; set; }
		public string InstructorImageUrl { get; set; }
		public DateTime InstructorCreatedDate { get; set; }
		public bool InstructorStatus { get; set; }

		public int InstructorLike { get; set; }
		public int InstructorDisLike { get; set; }
		public int InstructorPuan { get; set; }
		public int InstructorViewCountLog { get; set; }

		public ICollection<Course>? Courses { get; set; }
	}
}
