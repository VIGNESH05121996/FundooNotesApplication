using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ForgetPasswordModel
    {
        [Required(ErrorMessage ="Email is required")]
        [Display(Name ="Email")]
        public string Email { get; set; }
    }
}
