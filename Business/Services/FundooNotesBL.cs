// <copyright file="FundooNotesBL.cs" company="Fundoo Notes Application">
//     FundooNotesBL copyright tag.
// </copyright>

namespace Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Common.Models;
    using Common.NotesModels;
    using Microsoft.AspNetCore.Http;
    using Repository.Entities;
    using Repository.Interfaces;

    /// <summary>
    /// Business Layer Fundoo Notes
    /// </summary>
    /// <seealso cref="Business.Interfaces.IFundooNotesBL" />
    public class FundooNotesBL : IFundooNotesBL
    {
        /// <summary>
        /// The fundoo notes rl
        /// </summary>
        private IFundooNotesRL fundooNotesRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="FundooNotesBL"/> class.
        /// </summary>
        /// <param name="fundooNotesRL">The fundoo notes rl.</param>
        public FundooNotesBL(IFundooNotesRL fundooNotesRL)
        {
            this.fundooNotesRL = fundooNotesRL;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public GetNotesResponseModel CreateNotes(NotesModel model, long jwtUserId)
        {
            return this.fundooNotesRL.CreateNotes(model, jwtUserId);
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public IEnumerable<FundooNotes> GetAllNotes(long jwtUserId)
        {
            return this.fundooNotesRL.GetAllNotes(jwtUserId);
        }

        /// <summary>
        /// Gets the note with identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public GetNotesResponseModel GetNoteWithId(long notesId, long jwtUserId)
        {
            return this.fundooNotesRL.GetNoteWithId(notesId, jwtUserId);
        }

        /// <summary>
        /// Gets the notes with identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public FundooNotes GetNotesWithId(long notesId, long jwtUserId)
        {
            return this.fundooNotesRL.GetNotesWithId(notesId, jwtUserId);
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="updateNotes">The update notes.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public GetNotesResponseModel UpdateNotes(long notesId, FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId)
        {
            return this.fundooNotesRL.UpdateNotes(notesId, updateNotes, notes, jwtUserId);
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public void DeleteNotes(long notesId, FundooNotes notes, long jwtUserId)
        {
            this.fundooNotesRL.DeleteNotes(notesId, notes, jwtUserId);
        }

        /// <summary>
        /// Pinnings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public GetNotesResponseModel PinningNotes(long notesId, long jwtUserId)
        {
            return this.fundooNotesRL.PinningNotes(notesId, jwtUserId);
        }

        /// <summary>
        /// Archivings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public GetNotesResponseModel ArchivingNotes(long notesId, long jwtUserId)
        {
            return this.fundooNotesRL.ArchivingNotes(notesId, jwtUserId);
        }

        /// <summary>
        /// Trashings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public GetNotesResponseModel TrashingNotes(long notesId, long jwtUserId)
        {
            return this.fundooNotesRL.TrashingNotes(notesId, jwtUserId);
        }

        /// <summary>
        /// Colors the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="colorNotes">The color notes.</param>
        /// <param name="color">The color.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public void ColorNotes(long notesId, FundooNotes colorNotes, ColorModel color, long jwtUserId)
        {
            this.fundooNotesRL.ColorNotes(notesId, colorNotes, color, jwtUserId);
        }

        /// <summary>
        /// Images the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="imageNotes">The image notes.</param>
        /// <param name="image">The image.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public ImageResponseModel ImageNotes(long notesId, FundooNotes imageNotes, IFormFile image, long jwtUserId)
        {
            return this.fundooNotesRL.ImageNotes(notesId, imageNotes, image, jwtUserId);
        }

        /// <summary>
        /// Redises the notes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FundooNotes> RedisNotes()
        {
            return this.fundooNotesRL.RedisNotes();
        }
    }
}
