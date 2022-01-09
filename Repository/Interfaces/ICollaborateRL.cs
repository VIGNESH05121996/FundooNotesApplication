using Common.CollaboratorModels;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICollaborateRL
    {
        //void AddCollaborate(CollaborateModel model);
        FundooCollaborate GetCollabWithId(long collabId);
        void DeleteCollab(FundooCollaborate collabId);
        void AddCollaborate(long notesId, long jwtUserId,CollaborateModel model);
    }
}
