// <copyright file="CollaborateBL.cs" company="Fundoo Notes Application">
//     CollaborateBL copyright tag.
// </copyright>

namespace Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Common.CollaboratorModels;
    using Repository.Entities;
    using Repository.Interfaces;

    /// <summary>
    /// Business Layer Collaborator
    /// </summary>
    /// <seealso cref="Business.Interfaces.ICollaborateBL" />
    public class CollaborateBL : ICollaborateBL
    {
        /// <summary>
        /// The collaborate rl
        /// </summary>
        private readonly ICollaborateRL collaborateRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaborateBL"/> class.
        /// </summary>
        /// <param name="collaborateRL">The collaborate rl.</param>
        public CollaborateBL(ICollaborateRL collaborateRL)
        {
            this.collaborateRL = collaborateRL;
        }

        /// <summary>
        /// Gets the collab with identifier.
        /// </summary>
        /// <param name="collabId">The collab identifier.</param>
        public FundooCollaborate GetCollabWithId(long collabId)
        {
            return this.collaborateRL.GetCollabWithId(collabId);
        }

        /// <summary>
        /// Deletes the collab.
        /// </summary>
        /// <param name="collab">The collab.</param>
        public void DeleteCollab(FundooCollaborate collab)
        {
            this.collaborateRL.DeleteCollab(collab);
        }

        /// <summary>
        /// Adds the collaborate.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <param name="model">The model.</param>
        public CollabResponseModel AddCollaborate(long notesId, long jwtUserId, CollaborateModel model)
        {
            return this.collaborateRL.AddCollaborate(notesId, jwtUserId, model);
        }
    }
}
