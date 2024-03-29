﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.identity
{
	public class AppUser: IdentityUser<int>
	{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string?  Profession{ get; set; }
        public string? ImageUrl { get; set; }


		public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }

        public ICollection<Course>? Courses { get; set; }
        public ICollection<PurchasedCourse> PurchasedCourses { get; set; }
		public ICollection<WidgetClickLog> WidgetClickLogs { get; set; }
	}
}
