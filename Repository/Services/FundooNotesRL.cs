using Common.Models;
using Common.NotesModels;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class FundooNotesRL : IFundooNotesRL
    {
        readonly FundooUserContext context;
        public FundooNotesRL(FundooUserContext context)
        {
            this.context = context;
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
                FundooNotes notes = new()
                {
                    Title = model.Title,
                    Message = model.Message,
                    Color = model.Color,
                    Image = model.Image,
                    Archive = model.Archive,
                    Pin = model.Pin,
                    CreatedAt = model.CreatedAt,
                    UserId = jwtUserId
                };
                this.context.Add(notes);
                this.context.SaveChanges();
            }
            catch (Exception)
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var user = this.context.NotesTable.FirstOrDefault(e => e.UserId == jwtUserId);
                    GetNotesResposeModel model = new()
                    {
                        UserId=user.UserId,
                        NotesId=user.NotesId,
                        Title = user.Title,
                        Message=user.Message,
                        Color=user.Color,
                        Image=user.Image,
                        Pin=user.Pin,
                        Archive=user.Archive,
                        Trash=user.Trash,
                        CreatedAt=user.CreatedAt,
                        ModifiedAt=user.ModifiedAt
                    };
                    return model;
                }
                return null;
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var user= this.context.NotesTable.FirstOrDefault(i => i.NotesId == notesId && i.UserId == jwtUserId);
                    GetNotesResposeModel model = new()
                    {
                        UserId = user.UserId,
                        NotesId = user.NotesId,
                        Title = user.Title,
                        Message = user.Message,
                        Color = user.Color,
                        Image = user.Image,
                        Pin = user.Pin,
                        Archive = user.Archive,
                        Trash = user.Trash,
                        CreatedAt = user.CreatedAt,
                        ModifiedAt = user.ModifiedAt
                    };
                    return model;
                }
                return null;
            }
            catch (Exception)
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    return this.context.NotesTable.FirstOrDefault(i => i.NotesId == notesId && i.UserId == jwtUserId);
                }
                return null;
            }
            catch (Exception)
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
        public void UpdateNotes(FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    updateNotes.Title = notes.Title;
                    updateNotes.Message = notes.Message;
                    updateNotes.Image = notes.Image;
                    updateNotes.ModifiedAt = notes.ModifiedAt;
                    this.context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public void DeleteNotes(FundooNotes notes, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    this.context.NotesTable.Remove(notes);
                    this.context.SaveChanges();
                }
            }
            catch (Exception)
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var pinNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Pin == false);
                    if (pinNotes != null)
                    {
                        pinNotes.Pin = true;
                        this.context.SaveChanges();
                        var user = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.UserId == jwtUserId && e.Pin == true);
                        if (user != null)
                        {
                            GetNotesResposeModel model = new()
                            {
                                UserId = user.UserId,
                                NotesId = user.NotesId,
                                Title = user.Title,
                                Message = user.Message,
                                Color = user.Color,
                                Image = user.Image,
                                Pin = user.Pin,
                                Archive = user.Archive,
                                Trash = user.Trash,
                                CreatedAt = user.CreatedAt,
                                ModifiedAt = user.ModifiedAt
                            };
                            return model;
                        }
                    }
                    var unPinNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Pin == true);
                    if (unPinNotes != null)
                    {
                        unPinNotes.Pin = false;
                        this.context.SaveChanges();
                        var user = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.UserId == jwtUserId && e.Pin == false);
                        if (user != null)
                        {
                            GetNotesResposeModel model = new()
                            {
                                UserId = user.UserId,
                                NotesId = user.NotesId,
                                Title = user.Title,
                                Message = user.Message,
                                Color = user.Color,
                                Image = user.Image,
                                Pin = user.Pin,
                                Archive = user.Archive,
                                Trash = user.Trash,
                                CreatedAt = user.CreatedAt,
                                ModifiedAt = user.ModifiedAt
                            };
                            return model;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var archiveNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Archive == false);
                    if (archiveNotes != null)
                    {
                        archiveNotes.Archive = true;
                        this.context.SaveChanges();
                        var user = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.UserId == jwtUserId && e.Archive == true);
                        if (user != null)
                        {
                            GetNotesResposeModel model = new()
                            {
                                UserId = user.UserId,
                                NotesId = user.NotesId,
                                Title = user.Title,
                                Message = user.Message,
                                Color = user.Color,
                                Image = user.Image,
                                Pin = user.Pin,
                                Archive = user.Archive,
                                Trash = user.Trash,
                                CreatedAt = user.CreatedAt,
                                ModifiedAt = user.ModifiedAt
                            };
                            return model;
                        }
                    }
                    var unArchiveNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Archive == true);
                    if (unArchiveNotes != null)
                    {
                        unArchiveNotes.Archive = false;
                        this.context.SaveChanges();
                        var user = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.UserId == jwtUserId && e.Archive == false);
                        if (user != null)
                        {
                            GetNotesResposeModel model = new()
                            {
                                UserId = user.UserId,
                                NotesId = user.NotesId,
                                Title = user.Title,
                                Message = user.Message,
                                Color = user.Color,
                                Image = user.Image,
                                Pin = user.Pin,
                                Archive = user.Archive,
                                Trash = user.Trash,
                                CreatedAt = user.CreatedAt,
                                ModifiedAt = user.ModifiedAt
                            };
                            return model;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var trashNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Trash == false);
                    if (trashNotes != null)
                    {
                        trashNotes.Trash = true;
                        this.context.SaveChanges();
                        var user = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.UserId == jwtUserId && e.Trash == true);
                        if (user != null)
                        {
                            GetNotesResposeModel model = new()
                            {
                                UserId = user.UserId,
                                NotesId = user.NotesId,
                                Title = user.Title,
                                Message = user.Message,
                                Color = user.Color,
                                Image = user.Image,
                                Pin = user.Pin,
                                Archive = user.Archive,
                                Trash = user.Trash,
                                CreatedAt = user.CreatedAt,
                                ModifiedAt = user.ModifiedAt
                            };
                            return model;
                        }
                    }
                    var unTrashNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Trash == true);
                    if (unTrashNotes != null)
                    {
                        unTrashNotes.Trash = false;
                        this.context.SaveChanges();
                        var user = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.UserId == jwtUserId && e.Trash == false);
                        if (user != null)
                        {
                            GetNotesResposeModel model = new()
                            {
                                UserId = user.UserId,
                                NotesId = user.NotesId,
                                Title = user.Title,
                                Message = user.Message,
                                Color = user.Color,
                                Image = user.Image,
                                Pin = user.Pin,
                                Archive = user.Archive,
                                Trash = user.Trash,
                                CreatedAt = user.CreatedAt,
                                ModifiedAt = user.ModifiedAt
                            };
                            return model;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
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
        public void ColorNotes(FundooNotes colorNotes, ColorModel color, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    colorNotes.Color = color.Color;
                    this.context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
