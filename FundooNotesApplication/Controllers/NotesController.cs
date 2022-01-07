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
            catch (Exception ex)
            {
                return BadRequest(new { Success = false,Message=ex.Message,StackTraceException = ex.StackTrace });
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
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        [HttpGet("{notesId}")]
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
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        [HttpPut("{notesId}")]
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
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        [HttpDelete("{notesId}")]
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
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        [HttpPut("Pin/{notesId}")]
        public IActionResult PinningNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.GetNotesWithId(notesId, jwtUserId) == null)
                {
                    return BadRequest(new { Status = false, Message = "No Notes available"});
                }
                var result=notesBL.PinningNotes(notesId, jwtUserId);
                return Ok(new { Status = true, Message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        [HttpPut("Archive/{notesId}")]
        public IActionResult ArchivivingNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.GetNotesWithId(notesId, jwtUserId) == null)
                {
                    return BadRequest(new { Status = false, Message = "There is no notes with particuar NotesId" });
                }
                var result=notesBL.ArchivivingNotes(notesId, jwtUserId);
                return Ok(new { Status = true, Message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        [HttpPut("Trash/{notesId}")]
        public IActionResult TrashingNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.GetNotesWithId(notesId, jwtUserId) == null)
                {
                    return BadRequest(new { Status = false, Message = "There is no notes with particuar NotesId" });
                }
                var result = notesBL.TrashingNotes(notesId, jwtUserId);
                return Ok(new { Status = true, Message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        [HttpPut("Color/{notesId}")]
        public IActionResult ColorNotes(long notesId, ColorModel color)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes colorNotes = notesBL.GetNotesWithId(notesId, jwtUserId);
                if (colorNotes == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes Found With NotesId" });
                }
                notesBL.ColorNotes(colorNotes, color, jwtUserId);
                return Ok(new { Success = true, message = "Color Updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }
    }
}
