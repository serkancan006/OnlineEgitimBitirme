using EntityLayer.Concrete.Common;
using EntityLayer.Concrete.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class PurchasedCourse: BaseEntity
	{
		public int AppUserID { get; set; }
		//public AppUser AppUser { get; set; }
		public int CourseID { get; set; }
		public Course Course { get; set; }
	}
}
