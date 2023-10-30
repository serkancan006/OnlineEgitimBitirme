using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
	public class GenericRepository<T> : IGenericDal<T> where T : BaseEntity //class
	{
		public void Delete(T t)
		{
			using var c = new Context();
			c.Remove(t);
			c.SaveChanges();
		}

		public T GetByID(int id, bool tracking = true)
		{
            using var c = new Context();
            if (!tracking)
            {
                return c.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id == id);
            }
            return c.Set<T>().Find(id);
        }

		public List<T> GetList(bool tracking = true)
		{
			using var c = new Context();
            if (!tracking)
            {
                return c.Set<T>().AsNoTracking().ToList();
            }
            return c.Set<T>().ToList();
        }

		public List<T> GetListByFilter(System.Linq.Expressions.Expression<Func<T, bool>> filter, bool tracking = true)
		{
            using var c = new Context();
            if (!tracking)
            {
                return c.Set<T>().AsNoTracking().Where(filter).ToList();
            }
            return c.Set<T>().Where(filter).ToList();
        }

		public void Insert(T t)
		{
			using var c = new Context();
			c.Add(t);
			c.SaveChanges();
		}

		public void Update(T t)
		{
			using var c = new Context();
			c.Update(t);
            c.SaveChanges();
        }
	}
}
