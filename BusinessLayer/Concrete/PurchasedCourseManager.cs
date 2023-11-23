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
        private readonly UserManager<AppUser> _userManager;

        public PurchasedCourseManager(IPurchasedCourseDal purchasedCourseDal, UserManager<AppUser> userManager)
        {
            _purchasedCourseDal = purchasedCourseDal;
            _userManager = userManager;
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
        public async Task<List<PurchasedCourse>> GetListByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var values =  _purchasedCourseDal.GetListByFilter(x => x.AppUserID == user.Id);
                return values;
            }
            else
            {
                return new List<PurchasedCourse>();
            }
        }
    }
}
