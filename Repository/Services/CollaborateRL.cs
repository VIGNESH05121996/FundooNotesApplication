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

        public void AddCollaborate(long notesId, long jwtUserId,CollaborateModel model)
        {
            try
            {
                var validNotesAndUser = this.context.NotesTable.Where(e => e.UserId == jwtUserId);
                FundooCollaborate collaborate = new()
                {
                    NotesId = notesId,
                    UserId=jwtUserId,
                    Collaborated_Email = model.Collaborated_Email
                };
                this.context.Add(collaborate);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
