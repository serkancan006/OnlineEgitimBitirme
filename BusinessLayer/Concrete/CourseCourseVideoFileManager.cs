using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CourseCourseVideoFileManager : ICourseCourseVideoFileService
    {
        ICourseCourseVideoFileDal _CourseCourseVideoFileDal;
        public CourseCourseVideoFileManager(ICourseCourseVideoFileDal CourseCourseVideoFileDal)
        {
            _CourseCourseVideoFileDal = CourseCourseVideoFileDal;
        }

        public void TAdd(CourseCourseVideoFile t)
        {
            _CourseCourseVideoFileDal.Insert(t);
        }

        public void TDelete(CourseCourseVideoFile t)
        {
            _CourseCourseVideoFileDal.Delete(t);
        }

        public CourseCourseVideoFile TGetByID(int id)
        {
            return _CourseCourseVideoFileDal.GetByID(id, false);
        }

        public List<CourseCourseVideoFile> TGetList()
        {
            return _CourseCourseVideoFileDal.GetList(false);
        }

        public void TUpdate(CourseCourseVideoFile t)
        {
            _CourseCourseVideoFileDal.Update(t);
        }
    }
}
