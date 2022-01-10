using Business.Interfaces;
using Common.Models;
using Common.NotesModels;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class FundooNotesBL : IFundooNotesBL
    {
        public IFundooNotesRL fundooNotesRL;
        public FundooNotesBL(IFundooNotesRL fundooNotesRL)
        {
            this.fundooNotesRL = fundooNotesRL;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public void CreateNotes(NotesModel model, long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.CreateNotes(model, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public GetNotesResposeModel GetAllNotes(long jwtUserId)
        {
            try
            {
                return this.fundooNotesRL.GetAllNotes(jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the note with identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public GetNotesResposeModel GetNoteWithId(long notesId, long jwtUserId)
        {
            try
            {
                return this.fundooNotesRL.GetNoteWithId(notesId, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the notes with identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public FundooNotes GetNotesWithId(long notesId, long jwtUserId)
        {
            try
            {
                return this.fundooNotesRL.GetNotesWithId(notesId, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="updateNotes">The update notes.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public void UpdateNotes(FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.UpdateNotes(updateNotes, notes, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public void DeleteNotes(FundooNotes notes, long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.DeleteNotes(notes, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Pinnings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public GetNotesResposeModel PinningNotes(long notesId, long jwtUserId)
        {
            try
            {
                return this.fundooNotesRL.PinningNotes(notesId, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Archivivings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public GetNotesResposeModel ArchivivingNotes(long notesId, long jwtUserId)
        {
            try
            {
                return this.fundooNotesRL.ArchivivingNotes(notesId, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Trashings the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public GetNotesResposeModel TrashingNotes(long notesId, long jwtUserId)
        {
            try
            {
                return this.fundooNotesRL.TrashingNotes(notesId, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Colors the notes.
        /// </summary>
        /// <param name="colorNotes">The color notes.</param>
        /// <param name="color">The color.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public void ColorNotes(FundooNotes colorNotes, ColorModel color, long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.ColorNotes(colorNotes, color, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
