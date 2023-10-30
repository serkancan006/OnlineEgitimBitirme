using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
	public interface IGenericDal<T>
	{
		void Insert(T t);
		void Delete(T t);
		void Update(T t);
		List<T> GetList(bool tracking = true);
		T GetByID(int id, bool tracking = true);
		List<T> GetListByFilter(Expression<Func<T, bool>> filter, bool tracking = true);
	}
}
