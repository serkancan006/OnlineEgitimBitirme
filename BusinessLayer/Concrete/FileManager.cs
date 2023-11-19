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
    public class FileManager : IFileService
    {
        IFileDal _FileDal;
        public FileManager(IFileDal FileDal)
        {
            _FileDal = FileDal;
        }

        public void TAdd(EntityLayer.Concrete.File.File t)
        {
            _FileDal.Insert(t);
        }

        public void TDelete(EntityLayer.Concrete.File.File t)
        {
            _FileDal.Delete(t);
        }

        public EntityLayer.Concrete.File.File TGetByID(int id)
        {
            return _FileDal.GetByID(id, false);
        }

        public List<EntityLayer.Concrete.File.File> TGetList()
        {
            return _FileDal.GetList(false);
        }

        public List<EntityLayer.Concrete.File.File> TGetListTrueStatus()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(EntityLayer.Concrete.File.File t)
        {
            _FileDal.Update(t);
        }
    }
}
