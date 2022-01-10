using Common.CollaboratorModels;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class CollaborateRL : ICollaborateRL
    {
        readonly FundooUserContext context;
        public CollaborateRL(FundooUserContext context)
        {
            this.context = context;
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
                return this.context.CollaboratorTable.FirstOrDefault(e => e.CollaboratorId == collabId);
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
                this.context.CollaboratorTable.Remove(collab);
                this.context.SaveChanges();
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
                var validNotesAndUser = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                FundooCollaborate collaborate = new()
                {
                    NotesId = notesId,
                    UserId = jwtUserId,
                    Collaborated_Email = model.Collaborated_Email
                };
                this.context.Add(collaborate);
                this.context.SaveChanges();

                CollabResponseModel responseModel = new()
                {
                    CollaboratorId=collaborate.CollaboratorId,
                    NotesId=collaborate.NotesId,
                    UserId=collaborate.UserId,
                    Collaborated_Email=collaborate.Collaborated_Email

                };
                return responseModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
