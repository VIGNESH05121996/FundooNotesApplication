using Common.Models;
using Common.NotesModels;
using Microsoft.AspNetCore.Http;
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
        GetNotesResposeModel CreateNotes(NotesModel model, long jwtUserId);
        GetNotesResposeModel GetAllNotes(long jwtUserId);
        FundooNotes GetNotesWithId(long notesId, long jwtUserId);
        GetNotesResposeModel GetNoteWithId(long notesId, long jwtUserId);
        GetNotesResposeModel UpdateNotes(long notesId,FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId);
        void DeleteNotes(long notesId, FundooNotes notes, long jwtUserId);
        GetNotesResposeModel PinningNotes(long notesId, long jwtUserId);
        GetNotesResposeModel ArchivivingNotes(long notesId, long jwtUserId);
        GetNotesResposeModel TrashingNotes(long notesId, long jwtUserId);
        void ColorNotes(long notesId, FundooNotes colorNotes, ColorModel color, long jwtUserId);
        ImageResponseModel ImageNotes(long notesId,FundooNotes imageNotes, IFormFile image, long jwtUserId);
        IEnumerable<FundooNotes> RedisNotes();
    }
}
