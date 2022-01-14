// <copyright file="FundooLable.cs" company="Fundoo Notes Application">
//     FundooLable copyright tag.
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
    /// Entity for Lable Table
    /// </summary>
    public class FundooLable
    {
        /// <summary>
        /// Gets or sets the lable identifier.
        /// </summary>
        /// <value>
        /// The lable identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LableId { get; set; }

        /// <summary>
        /// Gets or sets the fundoo notes.
        /// </summary>
        /// <value>
        /// The fundoo notes.
        /// </value>
        public FundooNotes FundooNotes { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [ForeignKey("FundooNotes")]
        public long? NotesId { get; set; }

        /// <summary>
        /// Gets or sets the fundoo user.
        /// </summary>
        /// <value>
        /// The fundoo user.
        /// </value>
        public FundooUser FundooUser { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("FundooUser")]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the lable.
        /// </summary>
        /// <value>
        /// The name of the lable.
        /// </value>
        [DataType(DataType.EmailAddress)]
        public string Lable_Name { get; set; }
    }
}
