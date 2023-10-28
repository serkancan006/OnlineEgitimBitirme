using EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Contact: BaseEntity
	{
		public string Mail { get; set; }
		public string Adress { get; set; }
		public string Phone { get; set; }
		public string MapLocation { get; set; }
	}
}
