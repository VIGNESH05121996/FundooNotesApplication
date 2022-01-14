// <copyright file="ICollaborateRL.cs" company="Fundoo Notes Application">
//     ICollaborate copyright tag.
// </copyright>

namespace Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.CollaboratorModels;
    using Repository.Entities;

    /// <summary>
    /// Repository Interfaces for Collaborator
    /// </summary>
    public interface ICollaborateRL
    {
        /// <summary>
        /// Gets the collab with identifier.
        /// </summary>
        /// <param name="collabId">The collab identifier.</param>
        FundooCollaborate GetCollabWithId(long collabId);

        /// <summary>
        /// Deletes the collab.
        /// </summary>
        /// <param name="collabId">The collab identifier.</param>
        void DeleteCollab(FundooCollaborate collabId);

        /// <summary>
        /// Adds the collaborate.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <param name="model">The model.</param>
        CollabResponseModel AddCollaborate(long notesId, long jwtUserId, CollaborateModel model);
    }
}
