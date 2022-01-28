// <copyright file="IFundooNotesBL.cs" company="Fundoo Notes Application">
//     IFundooNotesBL copyright tag.
// </copyright>

namespace Business.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using Common.NotesModels;
    using Microsoft.AspNetCore.Http;
    using Repository.Entities;

    /// <summary>
    /// Business Layer Interface for Fundoo Notes
    /// </summary>
    public interface IFundooNotesBL
    {
        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        GetNotesResponseModel CreateNotes(NotesModel model, long jwtUserId);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        IEnumerable<FundooNotes> GetAllNotes(long jwtUserId);

        /// <summary>
        /// Gets the notes with identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        FundooNotes GetNotesWithId(long notesId, long jwtUserId);

        /// <summary>
        /// Gets the note with identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        GetNotesResponseModel GetNoteWithId(long notesId, long jwtUserId);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="updateNotes">The update notes.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        GetNotesResponseModel UpdateNotes(long notesId, FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId);

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        void DeleteNotes(long notesId, FundooNotes notes, long jwtUserId);

        /// <summary>
        /// Pinnings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        GetNotesResponseModel PinningNotes(long notesId, long jwtUserId);

        /// <summary>
        /// Archivings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        GetNotesResponseModel ArchivingNotes(long notesId, long jwtUserId);

        /// <summary>
        /// Trashings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        GetNotesResponseModel TrashingNotes(long notesId, long jwtUserId);

        /// <summary>
        /// Colors the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="colorNotes">The color notes.</param>
        /// <param name="color">The color.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        void ColorNotes(long notesId, FundooNotes colorNotes, ColorModel color, long jwtUserId);

        /// <summary>
        /// Images the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="imageNotes">The image notes.</param>
        /// <param name="image">The image.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        ImageResponseModel ImageNotes(long notesId, FundooNotes imageNotes, IFormFile image, long jwtUserId);

        /// <summary>
        /// Redises the notes.
        /// </summary>
        IEnumerable<FundooNotes> RedisNotes();
    }
}
