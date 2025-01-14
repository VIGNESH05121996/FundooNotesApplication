﻿// <copyright file="FundooNotes.cs" company="Fundoo Notes Application">
//     FundooNotes copyright tag.
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
    /// Entity for Notes Table
    /// </summary>
    public class FundooNotes
    {
        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Display(Name = "Message")]
        [DataType(DataType.Text)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the remainder.
        /// </summary>
        /// <value>
        /// The remainder.
        /// </value>
        [Display(Name = "Remainder")]
        [DataType(DataType.DateTime)]
        public DateTime? Remainder { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [Display(Name = "Color")]
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        [Display(Name = "Image")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FundooNotes"/> is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if archive; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Archive")]
        public bool Archive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FundooNotes"/> is pin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if pin; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Pin")]
        public bool Pin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FundooNotes"/> is trash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if trash; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Trash")]
        public bool Trash { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        [Display(Name = "CreatedAt")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the modified at.
        /// </summary>
        /// <value>
        /// The modified at.
        /// </value>
        [Display(Name = "ModifiedAt")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the fundoo user.
        /// </summary>
        /// <value>
        /// The fundoo user.
        /// </value>
        [ForeignKey("UserId")]
        public FundooUser FundooUser { get; set; }

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
