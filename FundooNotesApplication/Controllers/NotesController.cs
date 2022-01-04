using Business.Interfaces;
using Common.Models;
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
                var valid = HttpContext.User;
                long userId = Convert.ToInt32(valid.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (model == null)
                {
                    return BadRequest(new { Success = false, message = "No Data in notes" });
                }
                notesBL.CreateNotes(model);
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
                IEnumerable<FundooNotes> notes = notesBL.GetAllNotes();
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
                FundooNotes notes = notesBL.GetNotesWithId(notesId);
                if (notes == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes With Particular NotesId " });
                }
                return Ok(new { Success = true, message = "Retrived Notes With Particular ", notes });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Id")]
        public IActionResult UpdateNotes(long notesId, FundooNotes notes)
        {
            try
            {
                FundooNotes updateNotes = notesBL.GetNotesWithId(notesId);
                if (updateNotes == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes Found With NotesId" });
                }
                notesBL.UpdateNotes(updateNotes, notes);
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
                FundooNotes notes = notesBL.GetNotesWithId(notesId);
                if (notes == null)
                {
                    return BadRequest(new { Success = false, message = "Notes with entered notesId not found" });
                }
                notesBL.DeleteNotes(notes);
                return Ok(new { Success = true, message = "Notes Deleted From DataBase" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
