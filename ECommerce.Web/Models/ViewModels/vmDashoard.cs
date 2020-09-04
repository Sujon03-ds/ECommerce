using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models.ViewModels
{
    public class vmDashoard
    {
        public string CompanyLogo { get; set; }
        public string CompanyName { get; set; }
        public bool IsCompany { get; set; }
        public string Address { get; set; }

        //
        public int TotalCustomer { get; set; }
        public int TotalSupplier { get; set; }


    }
}