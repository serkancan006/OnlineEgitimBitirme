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
    public class WidgetClickLogManager : IWidgetClickLogService
    {
        private readonly IWidgetClickLogDal _widgetClickLogDal;
        public WidgetClickLogManager(IWidgetClickLogDal widgetClickLogDal)
        {
            _widgetClickLogDal = widgetClickLogDal;
        }

        public void TAdd(WidgetClickLog t)
        {
            _widgetClickLogDal.Insert(t);
        }

        public void TDelete(WidgetClickLog t)
        {
            _widgetClickLogDal.Delete(t);
        }

        public WidgetClickLog TGetByID(int id)
        {
            return _widgetClickLogDal.GetByID(id, false);
        }

        public List<WidgetClickLog> TGetList()
        {
            return _widgetClickLogDal.GetList(false);
        }

        public List<WidgetClickLog> TGetListTrueStatus()
        {
            return _widgetClickLogDal.GetListByFilter(x => x.Status == true);
        }

        public void TUpdate(WidgetClickLog t)
        {
            _widgetClickLogDal.Update(t);
        }

        public List<WidgetClickLog> TWidgetListIncludeTrueStatus()
        {
            return _widgetClickLogDal.WidgetListIncludeTrueStatus();
        }

        public bool TGetByAppUserIDAndCourseID(int appUserID, int courseID)
        {
            return _widgetClickLogDal.GetByAppUserIDAndCourseID(appUserID,courseID);
        }
    }
}
