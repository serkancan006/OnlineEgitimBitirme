using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly ICourseDal _courseDal;

        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }

        public void TAdd(Course t)
        {
            _courseDal.Insert(t);
        }

        public void TDelete(Course t)
        {
            _courseDal.Delete(t);
        }

        public Course TGetByID(int id)
        {
            return _courseDal.GetByID(id, false);
        }

        public List<Course> TGetList()
        {
            return _courseDal.GetList(false);
        }

        public List<Course> TGetListTrueStatus()
        {
            return _courseDal.GetListByFilter(x => x.Status == true);
        }

        public List<Course> TGetListByInstructor(int id)
        {
            return _courseDal.GetListByFilter(x => x.AppUserID == id);
        }

        public void TUpdate(Course t)
        {
            _courseDal.Update(t);
        }

        public List<Course> TGetListInclude()
        {
            return _courseDal.CourseListInclude();
        }
    }
}
