using ECommerce.Web.App_Start;
using ECommerce.Web.Managers;
using ECommerce.Web.Models;
using ECommerce.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {

        private ICustomerManager _customerManager;
        private IOrderManager _ordeManager;


        public OrderController(ICustomerManager _customerManager, IOrderManager _ordeManager)
        {

            this._customerManager = _customerManager;
            this._ordeManager = _ordeManager;
        }
        
        public ActionResult Index(string currentFilter, string searchString, string customerName, int? page = 1, int? NoOfRows = 10)
        {
            
            if (page < 1)
            {
                page = 1;
            }

            if (searchString != null )
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            ViewBag.page = page;
            ViewBag.CurrentFilter = searchString;
            ViewBag.customerName = customerName;
            ViewBag.NoOfRows = NoOfRows;
            return View(_ordeManager.GetOrderPageList(page.Value, NoOfRows.Value,  searchString, customerName));

        }

        // GET: Users/Create
        public ActionResult Create()
        {
            try
            {
                Order model = new Order();
                model.Id = 0;
                model.OrderDate = DateTime.Now;
                ViewBag.CustomerId = new SelectList(_customerManager.GetCustomerDropDown(), "Id", "Name");
                return View(model);
            }
            catch (Exception ex)
            {
                this.error(ex.Message.ToString());
                return RedirectToAction("Index");
            }

        }

    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Customer customer=_customerManager.GetById(order.CustomerId);
                    //order.OrderName = customer.Name+"-"+order.OrderName + "-" + order.OrderDate.ToShortDateString();
                    order.CreatedOn = DateTime.Now;
                    _ordeManager.Add(order);
                    _ordeManager.Save();
                    this.save();
                    return RedirectToAction("Index");
                }
                ViewBag.CustomerId = new SelectList(_customerManager.GetCustomerDropDown(), "Id", "Name", order.CustomerId);
                return View(order);
            }
            catch (Exception ex)
            {
                this.error(ex.Message.ToString());
                return RedirectToAction("Index");
            }

        }
     
        public ActionResult Edit(int? id)
        {
            if (id == 0)
            {
                this.error("Invalid try...");
                return RedirectToAction("Index");
            }
            Order order = _ordeManager.GetById(id.Value);
            if (order != null)
            {
                ViewBag.CustomerId = new SelectList(_customerManager.GetCustomerDropDown(), "Id", "Name", order.CustomerId);
                return View(order);

            }
            else
            {
                this.error("Invalid try...");
                return RedirectToAction("Index");
            }

        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (_ordeManager.CheckIfExistForUpdate(order.Id, order.OrderName))
                    //{
                    //    this.error("Customer with Phone Number <b> '" + order.PhoneNo + "'</b> alresdy exists.");
                    //    return View(order);
                    //}
                    //Customer customer = _customerManager.GetById(order.CustomerId);
                    //order.OrderName = customer.Name + "-" + order.OrderName + "-" + order.OrderDate.ToShortDateString();
                    //order.CreatedOn = DateTime.Now;
                    _ordeManager.Update(order);
                    _ordeManager.Save();
                    this.update();
                    return RedirectToAction("Index");
                }
                ViewBag.CustomerId = new SelectList(_customerManager.GetCustomerDropDown(), "Id", "Name", order.CustomerId); ViewBag.CustomerId = new SelectList(_customerManager.GetCustomerDropDown(), "Id", "Name", order.CustomerId);
                return View(order);
            }
            catch (Exception ex)
            {
                this.error(ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

  
        public ActionResult Delete(int? id)
        {

            if (id == 0)
            {
                this.error("Invalid try...");
                return RedirectToAction("Index");
            }
            Order order = _ordeManager.GetById(id.Value);
            if (order != null)
            {
                _ordeManager.Delete(order);
                _ordeManager.Save();
                this.delete();
                return RedirectToAction("Index");
            }
            else
            {
                this.error("Invalid try...");
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult Details(int? id)
        {
            if (id == 0)
            {
                this.error("Invalid try...");
                return RedirectToAction("Index");
            }
            Order order = _ordeManager.GetById(id.Value);
            if (order != null)
            {
                return View(order);
            }
            else
            {
                this.error("Invalid try...");
                return RedirectToAction("Index");
            }
        }


        public ActionResult AddCustomer()
        {
            Customer model = new Customer();
            model.Id = 0;
            return PartialView("_addCustomer", model);
        }
        [HttpPost]
        public ActionResult SaveCustomer(Customer customer)
        {
            vmStatus status = new vmStatus();
            try
            {
                
                if (ModelState.IsValid)
                {
                    
                    if (_customerManager.CheckIfExist(customer.PhoneNo))
                    {
                        status.Status = "200";
                        status.Msg = "Customer with Phone Number <b> '" + customer.PhoneNo + "'</b> alresdy exists.";
                        return Json(status, JsonRequestBehavior.AllowGet);
                    }
                    _customerManager.Add(customer);
                    _customerManager.Save();
                    status.Status = "400";
                    status.Msg = "Customer Added Successfull;";
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                status.Status = "200";
                status.Msg = "Please provide valid input for all required field";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                status.Status = "200";
                status.Msg = "Operation Failed";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCustomers()
        {
            var customers=_customerManager.GetCustomerDropDown();
            return Json(customers, JsonRequestBehavior.AllowGet);
        }
    }
}