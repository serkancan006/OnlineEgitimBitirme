using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PurchasedCourseManager : IPurchasedCourseService
    {
        private readonly IPurchasedCourseDal _purchasedCourseDal;

        public PurchasedCourseManager(IPurchasedCourseDal purchasedCourseDal)
        {
            _purchasedCourseDal = purchasedCourseDal;
        }

        public void TAdd(PurchasedCourse t)
        {
            _purchasedCourseDal.Insert(t);
        }

        public void TDelete(PurchasedCourse t)
        {
            _purchasedCourseDal.Delete(t);
        }

        public PurchasedCourse TGetByID(int id)
        {
            return _purchasedCourseDal.GetByID(id, false);
        }

        public List<PurchasedCourse> TGetList()
        {
            return _purchasedCourseDal.GetList(false);
        }

        public List<PurchasedCourse> TGetListTrueStatus()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(PurchasedCourse t)
        {
            _purchasedCourseDal.Update(t);
        }
        public List<PurchasedCourse> TCourseListInclude()
        {
            return _purchasedCourseDal.CourseListInclude();
        }
    }
}
