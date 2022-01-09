using Common.CollaboratorModels;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICollaborateBL
    {
        //void AddCollaborate(CollaborateModel model);
        FundooCollaborate GetCollabWithId(long collabId);
        void DeleteCollab(FundooCollaborate collab);
        void AddCollaborate(long notesId, long jwtUserId,CollaborateModel model);
    }
}
