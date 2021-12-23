using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z]{3,}", ErrorMessage = "First Name should contain minimum 3 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z]{1,}", ErrorMessage = "Last Name should not be empty")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9]+[+-._]?[a-zA-Z0-9]*[+-._]?[a-zA-Z0-9]+@[a-zA-Z0-9]+[.]{1}[a-zA-Z]{2,3}[.]?[a-zA-Z]{0,3}$", ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is required")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password should contain minimum 6 characters")]
        [Display(Name = "Password Name")]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }
    }
}
