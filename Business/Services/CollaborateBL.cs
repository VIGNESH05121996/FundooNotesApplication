using Business.Interfaces;
using Common.CollaboratorModels;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CollaborateBL : ICollaborateBL
    {
        readonly ICollaborateRL collaborateRL;
        public CollaborateBL(ICollaborateRL collaborateRL)
        {
            this.collaborateRL = collaborateRL;
        }

        /// <summary>
        /// Gets the collab with identifier.
        /// </summary>
        /// <param name="collabId">The collab identifier.</param>
        /// <returns></returns>
        public FundooCollaborate GetCollabWithId(long collabId)
        {
            try
            {
                return this.collaborateRL.GetCollabWithId(collabId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the collab.
        /// </summary>
        /// <param name="collab">The collab.</param>
        public void DeleteCollab(FundooCollaborate collab)
        {
            try
            {
                this.collaborateRL.DeleteCollab(collab);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds the collaborate.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public CollabResponseModel AddCollaborate(long notesId, long jwtUserId,CollaborateModel model)
        {
            try
            {
                return this.collaborateRL.AddCollaborate(notesId,jwtUserId,model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
