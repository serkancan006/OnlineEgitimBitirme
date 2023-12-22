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
    public class ContactUsManager : IContactUsService
    {
        IContactUsDal _ContactUsDal;
        public ContactUsManager(IContactUsDal ContactUsDal)
        {
            _ContactUsDal = ContactUsDal;
        }

        public void TAdd(ContactUs t)
        {
            _ContactUsDal.Insert(t);
        }

        public void TDelete(ContactUs t)
        {
            _ContactUsDal.Delete(t);
        }

        public ContactUs TGetByID(int id)
        {
            return _ContactUsDal.GetByID(id, false);
        }

        public List<ContactUs> TGetList()
        {
            return _ContactUsDal.GetList(false);
        }

        public List<ContactUs> TGetListTrueStatus()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(ContactUs t)
        {
            _ContactUsDal.Update(t);
        }
    }
}
