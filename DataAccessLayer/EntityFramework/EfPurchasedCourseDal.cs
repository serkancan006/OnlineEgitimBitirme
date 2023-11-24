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
	public class EfPurchasedCourseDal : GenericRepository<PurchasedCourse>, IPurchasedCourseDal
	{
        public List<PurchasedCourse> CourseListInclude()
        {
            var context = new Context();
            return context.PurchasedCourses.Include(x => x.Course).ToList();
        }
    }
}
