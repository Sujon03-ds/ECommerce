using ECommerce.Web.App_Start;
using ECommerce.Web.Managers;
using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {

        private ICustomerManager _customerManager;


        public CustomerController(ICustomerManager _customerManager)
        {

            this._customerManager = _customerManager;
        }
        
        public ActionResult Index(string currentFilter, string searchString, int? page = 1, int? NoOfRows = 10)
        {
            
            if (page < 1)
            {
                page = 1;
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.page = page;
            ViewBag.CurrentFilter = searchString;
            ViewBag.NoOfRows = NoOfRows;
            return View(_customerManager.GetCustomerPageList(page.Value, NoOfRows.Value,  searchString));

        }

        // GET: Users/Create
        public ActionResult Create()
        {
            try
            {
                Customer model = new Customer();
                model.Id = 0;
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
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_customerManager.CheckIfExist(customer.PhoneNo))
                    {
                        this.error("Customer with Phone Number <b> '" + customer.PhoneNo + "'</b> alresdy exists.");
                        return View(customer);
                    }
                    _customerManager.Add(customer);
                    _customerManager.Save();
                    this.save();
                    return RedirectToAction("Index");
                }
                return View(customer);
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
            Customer customer = _customerManager.GetById(id.Value);
            if (customer != null)
            {
                return View(customer);

            }
            else
            {
                this.error("Invalid try...");
                return RedirectToAction("Index");
            }

        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_customerManager.CheckIfExistForUpdate(customer.Id, customer.PhoneNo))
                    {
                        this.error("Customer with Phone Number <b> '" + customer.PhoneNo + "'</b> alresdy exists.");
                        return View(customer);
                    }
                    _customerManager.Update(customer);
                    _customerManager.Save();
                    this.update();
                    return RedirectToAction("Index");
                }
                return View(customer);
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
            Customer customer = _customerManager.GetById(id.Value);
            if (customer != null)
            {
                _customerManager.Delete(customer);
                _customerManager.Save();
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
            Customer customer = _customerManager.GetById(id.Value);
            if (customer != null)
            {
                return View(customer);
            }
            else
            {
                this.error("Invalid try...");
                return RedirectToAction("Index");
            }
        }
    }
}