using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models.ViewModels
{
    public class TO_IndexOrder
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public double Amount { get; set; }
        public bool DeliveryStatus { get; set; }
        public DateTime OrderDate { get; set; }
    }
}