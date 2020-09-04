using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.App_Start
{
    public class BaseController : Controller
    {
        private string cc = "111";
        protected void save()
        {
            TempData["save"] = "1";
        }
        protected void update()
        {
            TempData["update"] = "1";
        }
        protected void delete()
        {
            TempData["delete"] = "1";
        }
        protected void error(string msg)
        {
            TempData["error"] = msg;
        }
        protected void warning(string msg)
        {
            TempData["warning"] = msg;
        }
        protected void success(string msg)
        {
            TempData["success"] = msg;
        }


    }
}