using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EfCourseDal : GenericRepository<Course>, ICourseDal
	{
        public List<Course> CourseListInclude()
        {
            var context = new Context();
            return context.Courses.Include(x => x.AppUser).Include(y => y.Location).ToList();
        }
    }
}
