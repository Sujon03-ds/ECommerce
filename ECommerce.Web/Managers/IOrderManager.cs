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
        IPagedList<Order> GetPageList(int pageNo, int rowNo, string searchString,string customerName);
        IPagedList<TO_IndexOrder> GetOrderPageList(int pageNo, int rowNo, string searchString, string customerName);
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

        public IPagedList<Order> GetPageList(int pageNo, int rowNo, string searchString,string customerName)
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
        public IPagedList<TO_IndexOrder> GetOrderPageList(int pageNo, int rowNo, string searchString, string customerName)
        {
            string prm = string.Empty;
            if (!string.IsNullOrEmpty(searchString))
            {
                DateTime dtt = DateTime.Now;
                if (DateTime.TryParse(searchString.Trim(), out dtt)) { }
                prm += string.Format(@" and CONVERT(date,o.OrderDate)=CONVERT(date,'{0}')", dtt);
            }
            if (!string.IsNullOrEmpty(customerName))
            {
                prm += string.Format(@" and c.Name like '{0}%'", customerName.Trim());
            }
            string sqlTotalRows = string.Format(@"SELECT COUNT(*) FROM dbo.Orders o 
                    INNER JOIN dbo.Customers c ON c.Id = o.CustomerId WHERE o.Amount>{0} {1}", 0, prm);
            string sqlQuery = string.Format(@"SELECT o.Id,o.OrderName,o.OrderDate,o.Amount,o.DeliveryStatus
                    ,c.Name AS CustomerName,c.PhoneNo AS CustomerPhoneNo FROM dbo.Orders o 
                    INNER JOIN dbo.Customers c ON c.Id = o.CustomerId WHERE o.Amount>{0} {1}", 0, prm);
            var data = db.Database.SqlQuery<TO_IndexOrder>(sqlQuery).ToList().OrderByDescending(i => i.Id);
            int totalRows = db.Database.SqlQuery<int>(sqlTotalRows).FirstOrDefault();

            return new StaticPagedList<TO_IndexOrder>(data, pageNo, rowNo, totalRows);
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