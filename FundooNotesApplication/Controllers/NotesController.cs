using Business.Interfaces;
using Common.Models;
using Common.NotesModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public readonly IFundooNotesBL notesBL;
        public NotesController(IFundooNotesBL notesBL)
        {
            this.notesBL = notesBL;
        }

        [HttpPost]
        public IActionResult CreateNotes(NotesModel model)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (model == null)
                {
                    return BadRequest(new { Success = false, message = "No Data in notes" });
                }
                notesBL.CreateNotes(model,jwtUserId);
                return Ok(new { Success = true, message = "Notes Created Successfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetAllData()
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                IEnumerable<FundooNotes> notes = notesBL.GetAllNotes(jwtUserId);
                if (notes == null)
                {
                    return BadRequest(new { Success = false, message = "No notes in database " });
                }
                return Ok(new { Success = true, message = "Retrived All Notes ", notes });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("Id")]
        public IActionResult GetNotesWithId(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes notes = notesBL.GetNotesWithId(notesId,jwtUserId);
                if (notes == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes With Particular NotesId " });
                }
                return Ok(new { Success = true, message = "Retrived Notes With Particular NotesId ", notes });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Id")]
        public IActionResult UpdateNotes(long notesId, UpdateNotesModel notes)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes updateNotes = notesBL.GetNotesWithId(notesId,jwtUserId);
                if (updateNotes == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes Found With NotesId" });
                }
                notesBL.UpdateNotes(updateNotes, notes,jwtUserId);
                return Ok(new { Success = true, message = "Notes Updated Sucessfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("Id")]
        public IActionResult DeleteNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes notes = notesBL.GetNotesWithId(notesId,jwtUserId);
                if (notes == null)
                {
                    return BadRequest(new { Success = false, message = "Notes with entered notesId not found" });
                }
                notesBL.DeleteNotes(notes,jwtUserId);
                return Ok(new { Success = true, message = "Notes Deleted From DataBase" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Pin")]
        public IActionResult PinNotes(long noteId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                long notesId = notesBL.GetNotesWithId(noteId, jwtUserId).NotesId;
                if (notesId == 0)
                {
                    return BadRequest(new { Status = false, Message = "There is no notes with particuar NotesId"});
                }
                notesBL.PinNotes(notesId, jwtUserId);
                return Ok(new { Status = true, Message = " Note Pinned" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("UnPin")]
        public IActionResult UnPinNotes(long noteId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                long notesId = notesBL.GetNotesWithId(noteId, jwtUserId).NotesId;
                if (notesId > 0)
                {
                    notesBL.UnPinNotes(notesId, jwtUserId);
                    return Ok(new { Status = true, Message = "Note UnPinned" });
                }
                return BadRequest(new { Status = false, Message = "There is no notes with particuar NotesId" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Archive")]
        public IActionResult ArchiveNotes(long noteId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                long notesId = notesBL.GetNotesWithId(noteId, jwtUserId).NotesId;
                if (notesId == 0)
                {
                    return BadRequest(new { Status = false, Message = "There is no notes with particuar NotesId" });
                }
                notesBL.ArchiveNotes(notesId, jwtUserId);
                return Ok(new { Status = true, Message = "Note Archived" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("UnArchive")]
        public IActionResult UnArchiveNotes(long noteId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                long notesId = notesBL.GetNotesWithId(noteId, jwtUserId).NotesId;
                if (notesId > 0)
                {
                    notesBL.UnArchiveNotes(notesId, jwtUserId);
                    return Ok(new { Status = true, Message = "Note UnArchived" });
                }
                return BadRequest(new { Status = false, Message = "There is no notes with particuar NotesId" });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
