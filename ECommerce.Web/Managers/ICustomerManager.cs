using ECommerce.Web.Managers;
using ECommerce.Web.Models;
using ECommerce.Web.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Managers
{
    public interface ICustomerManager:IBaseManager<Customer>, IDisposable
    {
        bool Save();
        bool CheckIfExist(string PhoneNo);
        bool CheckIfExistForUpdate(int id, string PhonoNo);
        List<vmDropDownList> GetCustomerDropDown();
        IPagedList<Customer> GetCustomerPageList(int pageNo, int rowNo, string searchString);
    }
    public class CustomerManager:BaseManager<Customer>, ICustomerManager
    {
        private EComEntities db;

        public CustomerManager(DbContext db) : base(db)
        {
            this.db = (EComEntities)db;
        }


        public bool Save()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckIfExist(string PhonoNo)
        {

            int count = db.Customers.Where(c => c.PhoneNo == PhonoNo).Count();
            if (count > 0)
                return true;
            return false;
        }

        public bool CheckIfExistForUpdate(int id, string PhonoNo)
        {

            int count = db.Customers.Where(c => c.PhoneNo.Trim() == PhonoNo.Trim() && c.Id != id).Count();
            if (count > 0)
                return true;
            return false;
        }

        public List<vmDropDownList> GetCustomerDropDown()
        {

            List<vmDropDownList> list = (from c in db.Customers
                                         select new vmDropDownList
                                         {
                                             Id = c.Id,
                                             Name = c.Name + "-" + c.PhoneNo
                                         }).ToList();
            list = list.OrderBy(a => a.Name).ToList();

            return (list);
        }

        public IPagedList<Customer> GetCustomerPageList(int pageNo, int rowNo, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                int totalRows = db.Customers.Count();
                var data = db.Customers.OrderByDescending(a => a.Id).Skip((pageNo - 1) * rowNo).Take(rowNo).ToList();
                return new StaticPagedList<Customer>(data.OrderBy(o => o.Name), pageNo, rowNo, totalRows);
            }
            else
            {
                int totalRows = db.Customers.Where(a => a.Name.StartsWith(searchString)).Count();
                var data = db.Customers.Where(a => a.Name.StartsWith(searchString)).OrderByDescending(a => a.Id).Skip((pageNo - 1) * rowNo).Take(rowNo).ToList();
                return new StaticPagedList<Customer>(data.OrderBy(o => o.Name), pageNo, rowNo, totalRows);

            }

        }

        #region DISPOSED
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}