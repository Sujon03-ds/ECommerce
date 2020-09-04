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
    public interface IOrderManager : IBaseManager<Order>, IDisposable
    {
        bool Save();
        bool CheckIfExist(string PhoneNo);
        bool CheckIfExistForUpdate(int id, string PhonoNo);
        List<vmDropDownList> GetCustomerDropDown();
        IPagedList<Order> GetCustomerPageList(int pageNo, int rowNo, string searchString);
    }
    public class OrderManager : BaseManager<Order>, IOrderManager
    {
        private EComEntities db;

        public OrderManager(DbContext db) : base(db)
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

        public IPagedList<Order> GetCustomerPageList(int pageNo, int rowNo, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                int totalRows = db.Orders.Count();
                var data = db.Orders.OrderByDescending(a => a.Id).Skip((pageNo - 1) * rowNo).Take(rowNo).ToList();
                return new StaticPagedList<Order>(data.OrderBy(o => o.Customer.Name), pageNo, rowNo, totalRows);
            }
            else
            {
                int totalRows = db.Orders.Where(a => a.Customer.Name.StartsWith(searchString)).Count();
                var data = db.Orders.Where(a => a.Customer.Name.StartsWith(searchString)).OrderByDescending(a => a.Id).Skip((pageNo - 1) * rowNo).Take(rowNo).ToList();
                return new StaticPagedList<Order>(data.OrderBy(o => o.Customer.Name), pageNo, rowNo, totalRows);

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