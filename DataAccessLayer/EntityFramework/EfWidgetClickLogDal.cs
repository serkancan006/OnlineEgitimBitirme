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
	public class EfWidgetClickLogDal : GenericRepository<WidgetClickLog>, IWidgetClickLogDal
	{
        public List<WidgetClickLog> WidgetListIncludeTrueStatus()
        {
            var context = new Context();
            return context.WidgetClickLogs.Include(x => x.Course).Where(y => y.Status == true).ToList();
        }
        public bool GetByAppUserIDAndCourseID(int appUserID, int courseID)
        {
            var context = new Context();
            var result = context.WidgetClickLogs
                               .Include(x => x.Course)
                               .Where(y => y.Status == true && y.CourseID == courseID && y.AppUserID == appUserID)
                               .FirstOrDefault();
            if (result != null)
            {
                result.CourseViewCount += 1;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
