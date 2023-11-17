using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCourseVideoFile : GenericRepository<CourseVideoFile>, ICourseVideoFileDal
    {
        public List<CourseVideoFile> GetVideoFiles(int id)
        {
            var context = new Context();
            return context.CourseVideoFiles.Where(x => x.CourseID == id).ToList();
        }
    }
}
