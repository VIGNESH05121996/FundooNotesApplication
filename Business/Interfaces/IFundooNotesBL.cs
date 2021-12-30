using Common.Models;
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
        void CreateNotes(NotesModel model);
        IEnumerable<FundooNotes> GetAllNotes();
        FundooNotes GetNotesWithId(long notesId);
        void UpdateNotes(FundooNotes updateNotes, FundooNotes notes);
        void DeleteNotes(FundooNotes notes);
    }
}
