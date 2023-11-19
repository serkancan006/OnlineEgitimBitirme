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
    public class CourseImageFileManager : ICourseImageFileService
    {
        ICourseImageFileDal _CourseImageFileDal;
        public CourseImageFileManager(ICourseImageFileDal CourseImageFileDal)
        {
            _CourseImageFileDal = CourseImageFileDal;
        }

        public void TAdd(CourseImageFile t)
        {
            _CourseImageFileDal.Insert(t);
        }

        public void TDelete(CourseImageFile t)
        {
            _CourseImageFileDal.Delete(t);
        }

        public CourseImageFile TGetByID(int id)
        {
            return _CourseImageFileDal.GetByID(id, false);
        }

        public List<CourseImageFile> TGetList()
        {
            return _CourseImageFileDal.GetList(false);
        }

        public List<CourseImageFile> TGetListTrueStatus()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(CourseImageFile t)
        {
            _CourseImageFileDal.Update(t);
        }
    }
}
