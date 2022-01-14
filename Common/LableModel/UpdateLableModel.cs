// <copyright file="UpdateLableModel.cs" company="Fundoo Notes Application">
//     UpdateLableModel copyright tag.
// </copyright>

namespace Common.LableModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Update Lable Model
    /// </summary>
    public class UpdateLableModel
    {
        /// <summary>
        /// Gets or sets the name of the lable.
        /// </summary>
        /// <value>
        /// The name of the lable.
        /// </value>
        public string Lable_Name { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        public long? NotesId { get; set; }
    }
}
