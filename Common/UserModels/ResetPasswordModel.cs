using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ResetPasswordModel
    {
        //[Required(ErrorMessage = "Email is not in database")]
        //[Display(Name = "Email")]
        //public string Email { get; set; }
        //public string Token { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Enter Password Again for confirmation")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }



    }
}
