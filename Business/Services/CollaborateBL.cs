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

        public void AddCollaborate(long notesId, long jwtUserId,CollaborateModel model)
        {
            try
            {
                this.collaborateRL.AddCollaborate(notesId,jwtUserId,model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
