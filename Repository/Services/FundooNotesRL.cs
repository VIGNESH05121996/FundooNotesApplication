using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common.Models;
using Common.NotesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class FundooNotesRL : IFundooNotesRL
    {
        readonly FundooUserContext context;
        private readonly IConfiguration _config;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public FundooNotesRL(FundooUserContext context, IConfiguration config, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.context = context;
            _config = config;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var user = this.context.NotesTable.FirstOrDefault(e => e.UserId == jwtUserId);
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    return this.context.NotesTable.FirstOrDefault(i => i.NotesId == notesId && i.UserId == jwtUserId);
                }
                return null;
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
        public void UpdateNotes(long notesId, FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    if (this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId) != null)
                    {
                        updateNotes.Title = notes.Title;
                        updateNotes.Message = notes.Message;
                        updateNotes.ModifiedAt = notes.ModifiedAt;
                        this.context.SaveChanges();
                    }
                }
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
        public void DeleteNotes(long notesId, FundooNotes notes, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    if (this.context.NotesTable.FirstOrDefault(e=>e.NotesId==notesId) != null)
                    {
                        this.context.NotesTable.Remove(notes);
                        this.context.SaveChanges();
                    }
                }
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
        public void ColorNotes(long notesId, FundooNotes colorNotes, ColorModel color, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    if (this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId) != null)
                    {
                        colorNotes.Color = color.Color;
                        this.context.SaveChanges();
                    }  
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Images the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="imageNotes">The image notes.</param>
        /// <param name="image">The image.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public ImageResponseModel ImageNotes(long notesId, FundooNotes imageNotes, IFormFile image, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    if (this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId) != null)
                    {
                        Account account = new Account(_config["Cloudinary:CloudName"], _config["Cloudinary:APIKey"], _config["Cloudinary:APISecret"]);
                        var imagePath = image.OpenReadStream();
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams imageParams = new ImageUploadParams()
                        {
                            File = new FileDescription(image.FileName, imagePath)
                        };
                        var uploadImage = cloudinary.Upload(imageParams).Url.ToString();
                        imageNotes.Image = uploadImage;
                        this.context.SaveChanges();
                        var user = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.UserId == jwtUserId);
                        if (user != null)
                        {
                            ImageResponseModel model = new()
                            {
                                Image = user.Image,
                            };
                            return model;
                        }
                    }  
                }
                return null;
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
        public IEnumerable<FundooNotes> RedisNotes()
        {
            try
            {
                return this.context.NotesTable.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
