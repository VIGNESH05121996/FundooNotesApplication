using Common.Models;
using Common.NotesModels;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IFundooNotesRL
    {
        void CreateNotes(NotesModel model,long jwtUserId);
        IEnumerable<FundooNotes> GetAllNotes(long jwtUserId);
        FundooNotes GetNotesWithId(long notesId, long jwtUserId);
        void UpdateNotes(FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId);
        void DeleteNotes(FundooNotes notes, long jwtUserId);
    }
}
