// <copyright file="LableResponseModel.cs" company="Fundoo Notes Application">
//     LableResponseModel copyright tag.
// </copyright>

namespace Common.LableModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Lable Response Model
    /// </summary>
    public class LableResponseModel
    {
        /// <summary>
        /// Gets or sets the lable identifier.
        /// </summary>
        /// <value>
        /// The lable identifier.
        /// </value>
        public long LableId { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        public long? NotesId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the lable.
        /// </summary>
        /// <value>
        /// The name of the lable.
        /// </value>
        public string Lable_Name { get; set; }
    }
}
