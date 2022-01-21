// <copyright file="NotesController.cs" company="Fundoo Notes Application">
//     NotesController copyright tag.
// </copyright>

namespace FundooNotesApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Common.Models;
    using Common.NotesModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using Repository.Entities;

    /// <summary>
    /// NotescController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The notes bl
        /// </summary>
        private readonly IFundooNotesBL notesBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesBL">The notes bl.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public NotesController(IFundooNotesBL notesBL,IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Response Body after creating notes</returns>
        [HttpPost]
        public IActionResult CreateNotes(NotesModel model)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (model == null)
                {
                    return NotFound(new { Success = false, message = "No Data in notes" });
                }
                GetNotesResponseModel notes = notesBL.CreateNotes(model, jwtUserId);
                return Ok(new { Success = true, message = "Notes Created Successfully", notes });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>Response Body of all notes</returns>
        [HttpGet]
        public IActionResult GetAllNotes()
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                GetNotesResponseModel notes = notesBL.GetAllNotes(jwtUserId);
                if (notes == null)
                {
                    return NotFound(new { Success = false, message = "No notes in database " });
                }
                return Ok(new { Success = true, message = "Retrived All Notes ", notes });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Gets the notes with identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>Response Body of notes with particular notesId</returns>
        [HttpGet("{notesId}")]
        public IActionResult GetNotesWithId(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                GetNotesResponseModel notes = notesBL.GetNoteWithId(notesId, jwtUserId);
                if (notes == null)
                {
                    return NotFound(new { Success = false, message = "No Notes With Particular NotesId " });
                }
                return Ok(new { Success = true, message = "Retrived Notes With Particular NotesId ", notes });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="notes">The notes.</param>
        /// <returns>Response Body after updating notes</returns>
        [HttpPut("{notesId}")]
        public IActionResult UpdateNotes(long notesId, UpdateNotesModel notes)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes updateNotes = notesBL.GetNotesWithId(notesId, jwtUserId);
                if (updateNotes == null)
                {
                    return NotFound(new { Success = false, message = "No Notes Found With NotesId" });
                }
                GetNotesResponseModel note = notesBL.UpdateNotes(notesId, updateNotes, notes, jwtUserId);
                return Ok(new { Success = true, message = "Notes Updated Sucessfully", note });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>Delete message</returns>
        [HttpDelete("{notesId}")]
        public IActionResult DeleteNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes notes = notesBL.GetNotesWithId(notesId, jwtUserId);
                if (notes == null)
                {
                    return NotFound(new { Success = false, message = "Notes with entered notesId not found" });
                }
                notesBL.DeleteNotes(notesId, notes, jwtUserId);
                return Ok(new { Success = true, message = "Notes Deleted From DataBase" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Pinnings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>Response Body of pinned notes</returns>
        [HttpPut("{notesId}/Pin")]
        public IActionResult PinningNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.GetNotesWithId(notesId, jwtUserId) == null)
                {
                    return NotFound(new { Status = false, message = "No Notes available" });
                }
                var result = notesBL.PinningNotes(notesId, jwtUserId);
                return Ok(new { Status = true, message ="Pinning Note", result });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Archivivings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>Response Body of archived notes</returns>
        [HttpPut("{notesId}/Archive")]
        public IActionResult ArchivivingNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.GetNotesWithId(notesId, jwtUserId) == null)
                {
                    return NotFound(new { Status = false, message = "There is no notes with particuar NotesId" });
                }
                var result = notesBL.ArchivingNotes(notesId, jwtUserId);
                return Ok(new { Status = true, message ="Archiving Note", result });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Trashings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>Response Body of trashed notes</returns>
        [HttpPut("{notesId}/Trash")]
        public IActionResult TrashingNotes(long notesId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.GetNotesWithId(notesId, jwtUserId) == null)
                {
                    return NotFound(new { Status = false, message = "There is no notes with particuar NotesId" });
                }
                var result = notesBL.TrashingNotes(notesId, jwtUserId);
                return Ok(new { Status = true, message ="Trashing Note", result });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Colors the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>Colored Message</returns>
        [HttpPut("{notesId}/Color")]
        public IActionResult ColorNotes(long notesId, ColorModel color)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes colorNotes = notesBL.GetNotesWithId(notesId, jwtUserId);
                if (colorNotes == null)
                {
                    return NotFound(new { Success = false, message = "No Notes Found With NotesId" });
                }
                notesBL.ColorNotes(notesId, colorNotes, color, jwtUserId);
                return Ok(new { Success = true, message = "Color Updated" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Images the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>Response Body cloudinary image URL</returns>
        [HttpPut("{notesId}/Image")]
        public IActionResult ImageNotes(long notesId,IFormFile image)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooNotes imageNotes = notesBL.GetNotesWithId(notesId, jwtUserId);
                if (imageNotes == null)
                {
                    return NotFound(new { Success = false, message = "No Notes Found With NotesId" });
                }
                ImageResponseModel imageDetails = notesBL.ImageNotes(notesId, imageNotes, image, jwtUserId);
                return Ok(new { Success = true, message = "Image Uploaded", imageDetails});
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Redises the catching fundoo notes.
        /// </summary>
        /// <returns>Response Body of all notes</returns>
        [HttpGet("redis")]
        public async Task<IActionResult> RedisCatchingFundooNotes()
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (jwtUserId != 0)
                {
                    var cacheKey = "fundoonotesList";
                    string serializedNotesList;
                    var notesList = new List<FundooNotes>();
                    var redisNotesList = await distributedCache.GetAsync(cacheKey);
                    if (redisNotesList != null)
                    {
                        serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                        notesList = JsonConvert.DeserializeObject<List<FundooNotes>>(serializedNotesList);
                    }
                    else
                    {
                        notesList = (List<FundooNotes>)notesBL.RedisNotes();
                        serializedNotesList = JsonConvert.SerializeObject(notesList);
                        redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                        return Ok(notesList);
                    }
                }
                return NotFound(new { Success = false, message = "No Notes Found With NotesId" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }
    }
}
