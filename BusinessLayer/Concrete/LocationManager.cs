using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class LocationManager : ILocationService
    {
        private readonly ILocationDal _locationDal;

        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        public void TAdd(Location t)
        {
            _locationDal.Insert(t);
        }

        public void TDelete(Location t)
        {
            _locationDal.Delete(t);
        }

        public Location TGetByID(int id)
        {
            return _locationDal.GetByID(id, false);
        }

        public List<Location> TGetList()
        {
            return _locationDal.GetList(false);
        }

        public List<Location> TGetListTrueStatus()
        {
            return _locationDal.GetListByFilter(x => x.Status == true);
        }

        public void TUpdate(Location t)
        {
            _locationDal.Update(t);
        }
    }
}
