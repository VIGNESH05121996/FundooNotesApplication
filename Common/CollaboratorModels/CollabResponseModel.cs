// <copyright file="CollabResponseModel.cs" company="Fundoo Notes Application">
//     CollabResponseModel copyright tag.
// </copyright>

namespace Common.CollaboratorModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Collaborator Response
    /// </summary>
    public class CollabResponseModel
    {
        /// <summary>
        /// Gets or sets the collaborator identifier.
        /// </summary>
        /// <value>
        /// The collaborator identifier.
        /// </value>
        public long CollaboratorId { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        public long NotesId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the collaborated email.
        /// </summary>
        /// <value>
        /// The collaborated email.
        /// </value>
        public string Collaborated_Email { get; set; }
    }
}
