using Common.Models;
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
        void CreateNotes(NotesModel model);
        IEnumerable<FundooNotes> GetAllNotes();
        FundooNotes GetNotesWithId(long notesId);
        void UpdateNotes(FundooNotes updateNotes, FundooNotes notes);
        void DeleteNotes(FundooNotes notes);
    }
}
