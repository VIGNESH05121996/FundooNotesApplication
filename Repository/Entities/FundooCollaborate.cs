// <copyright file="FundooCollaborate.cs" company="Fundoo Notes Application">
//     FundooCollaborate copyright tag.
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
    /// Entity for Collaboration table
    /// </summary>
    public class FundooCollaborate
    {
        /// <summary>
        /// Gets or sets the collaborator identifier.
        /// </summary>
        /// <value>
        /// The collaborator identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorId { get; set; }

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
        public long NotesId { get; set; }

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
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the collaborated email.
        /// </summary>
        /// <value>
        /// The collaborated email.
        /// </value>
        [DataType(DataType.EmailAddress)]
        public string Collaborated_Email { get; set; }
    }
}
