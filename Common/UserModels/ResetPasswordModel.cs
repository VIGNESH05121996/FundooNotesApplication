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
        [Required(ErrorMessage = "New Password is required")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Enter Password Again for confirmation")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
