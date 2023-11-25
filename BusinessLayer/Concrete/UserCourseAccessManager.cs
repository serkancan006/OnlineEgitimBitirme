using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserCourseAccessManager : IUserCourseAccessService
    {
        public bool HasAccessToCourse(int userId, int courseId)
        {
            var context = new Context();
            var hasAccess = context.PurchasedCourses.Any(x => x.AppUserID == userId && x.CourseID == courseId);
            return hasAccess;
        }
    }
}
