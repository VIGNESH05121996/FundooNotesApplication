using Common.Models;
using Common.NotesModels;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IFundooNotesBL
    {
        void CreateNotes(NotesModel model,long jwtUserId);
        IEnumerable<FundooNotes> GetAllNotes(long jwtUserId);
        FundooNotes GetNotesWithId(long notesId, long jwtUserId);
        void UpdateNotes(FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId);
        void DeleteNotes(FundooNotes notes, long jwtUserId);
        void PinNotes(long notesId, long jwtUserId);
        void UnPinNotes(long notesId, long jwtUserId);
        void ArchiveNotes(long notesId, long jwtUserId);
        void UnArchiveNotes(long notesId, long jwtUserId);
    }
}
