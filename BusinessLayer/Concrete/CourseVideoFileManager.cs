using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Concrete.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CourseVideoFileManager : ICourseVideoFileService
    {
        ICourseVideoFileDal _CourseVideoFileDal;
        public CourseVideoFileManager(ICourseVideoFileDal CourseVideoFileDal)
        {
            _CourseVideoFileDal = CourseVideoFileDal;
        }

        public List<CourseVideoFile> CourseGetVideoFiles(int id)
        {
            return _CourseVideoFileDal.GetListByFilter(x => x.CourseID == id);
        }

        public void TAdd(CourseVideoFile t)
        {
            _CourseVideoFileDal.Insert(t);
        }

        public void TDelete(CourseVideoFile t)
        {
            _CourseVideoFileDal.Delete(t);
        }

        public CourseVideoFile TGetByID(int id)
        {
            return _CourseVideoFileDal.GetByID(id, false);
        }

        public List<CourseVideoFile> TGetList()
        {
            return _CourseVideoFileDal.GetList(false);
        }

        public void TUpdate(CourseVideoFile t)
        {
            _CourseVideoFileDal.Update(t);
        }
    }
}
