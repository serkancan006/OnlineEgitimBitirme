using EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Location: BaseEntity
	{
		public string Address { get; set; }
		public ICollection<Course> Courses { get; set; }
	}
}
