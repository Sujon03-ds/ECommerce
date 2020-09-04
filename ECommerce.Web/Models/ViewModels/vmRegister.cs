using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models.ViewModels
{
    public class vmRegister
    {

        [Required]
        //[MaxLength(12)]
        //[MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile No. must be numeric")]
        public string Mobile { get; set; }


        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email address")]       
        public string Email { get; set; }

        public bool IsChecked { get; set; }
    }
}