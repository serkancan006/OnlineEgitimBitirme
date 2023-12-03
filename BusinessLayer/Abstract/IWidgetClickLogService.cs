using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IWidgetClickLogService : IGenericService<WidgetClickLog>
	{
        List<WidgetClickLog> TWidgetListIncludeTrueStatus();
        bool TGetByAppUserIDAndCourseID(int appUserID, int courseID);
    }
}
