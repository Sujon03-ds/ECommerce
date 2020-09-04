using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public DateTime TransectionDate { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}