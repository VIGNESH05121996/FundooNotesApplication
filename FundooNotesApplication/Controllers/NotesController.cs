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

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
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
                notesBL.CreateNotes(model, jwtUserId);
                return Ok(new { Success = true, message = "Notes Created Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllNotes()
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                GetNotesResposeModel notes = notesBL.GetAllNotes(jwtUserId);
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

        /// <summary>
        /// Gets the notes with identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns></returns>
        [HttpGet("{notesId}")]
        public IActionResult GetNotesWithId(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                GetNotesResposeModel notes = notesBL.GetNoteWithId(notesId, jwtUserId);
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

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="notes">The notes.</param>
        /// <returns></returns>
        [HttpPut("{notesId}")]
        public IActionResult UpdateNotes(long notesId, UpdateNotesModel notes)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes updateNotes = notesBL.GetNotesWithId(notesId, jwtUserId);
                if (updateNotes == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes Found With NotesId" });
                }
                notesBL.UpdateNotes(updateNotes, notes, jwtUserId);
                return Ok(new { Success = true, message = "Notes Updated Sucessfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns></returns>
        [HttpDelete("{notesId}")]
        public IActionResult DeleteNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes notes = notesBL.GetNotesWithId(notesId, jwtUserId);
                if (notes == null)
                {
                    return BadRequest(new { Success = false, message = "Notes with entered notesId not found" });
                }
                notesBL.DeleteNotes(notes, jwtUserId);
                return Ok(new { Success = true, message = "Notes Deleted From DataBase" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Pinnings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns></returns>
        [HttpPut("{notesId}/Pin")]
        public IActionResult PinningNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.GetNotesWithId(notesId, jwtUserId) == null)
                {
                    return BadRequest(new { Status = false, Message = "No Notes available" });
                }
                var result = notesBL.PinningNotes(notesId, jwtUserId);
                return Ok(new { Status = true, Message ="Pinning Note", result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Archivivings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns></returns>
        [HttpPut("{notesId}/Archive")]
        public IActionResult ArchivivingNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.GetNotesWithId(notesId, jwtUserId) == null)
                {
                    return BadRequest(new { Status = false, Message = "There is no notes with particuar NotesId" });
                }
                var result = notesBL.ArchivivingNotes(notesId, jwtUserId);
                return Ok(new { Status = true, Message ="Archiving Note", result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Trashings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns></returns>
        [HttpPut("{notesId}/Trash")]
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
                return Ok(new { Status = true, Message ="Trashing Note", result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Colors the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        [HttpPut("{notesId}/Color")]
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
