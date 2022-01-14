// <copyright file="FundooUser.cs" company="Fundoo Notes Application">
//     FundooUser copyright tag.
// </copyright>

namespace Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Entities for User
    /// </summary>
    public class FundooUser
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z]{3,}",ErrorMessage = "First Name should contain minimum 3 characters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessage = "Last Name Is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z]{3,}", ErrorMessage = "Last Name should not be empty")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9]+[+-._]?[a-zA-Z0-9]*[+-._]?[a-zA-Z0-9]+@[a-zA-Z0-9]+[.]{1}[a-zA-Z]{2,3}[.]?[a-zA-Z]{0,3}$", ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required(ErrorMessage = "Password Is required")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password should contain minimum 6 characters")]
        [Display(Name = "Password Name")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the modified at.
        /// </summary>
        /// <value>
        /// The modified at.
        /// </value>
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the fundoo notes.
        /// </summary>
        /// <value>
        /// The fundoo notes.
        /// </value>
        public virtual ICollection<FundooNotes> FundooNotes { get; set; }

        /// <summary>
        /// Gets or sets the fundoo collaborate.
        /// </summary>
        /// <value>
        /// The fundoo collaborate.
        /// </value>
        public virtual ICollection<FundooCollaborate> FundooCollaborate { get; set; }

        /// <summary>
        /// Gets or sets the fundoo lable.
        /// </summary>
        /// <value>
        /// The fundoo lable.
        /// </value>
        public virtual ICollection<FundooLable> FundooLable { get; set; }
    }
}