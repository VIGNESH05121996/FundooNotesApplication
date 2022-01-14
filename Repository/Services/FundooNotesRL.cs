// <copyright file="FundooNotesRL.cs" company="Fundoo Notes Application">
//     FundooNotesRL copyright tag.
// </copyright>

namespace Repository.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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

    /// <summary>
    /// Repository Layer Fundoo Notes
    /// </summary>
    /// <seealso cref="Repository.Interfaces.IFundooNotesRL" />
    public class FundooNotesRL : IFundooNotesRL
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly FundooUserContext context;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration config;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="FundooNotesRL"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public FundooNotesRL(FundooUserContext context, IConfiguration config, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.context = context;
            this.config = config;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Respose body of new created notes</returns>
        public GetNotesResponseModel CreateNotes(NotesModel model, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    FundooNotes notes = new ()
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
                    GetNotesResponseModel models = new ()
                    {
                        UserId = notes.UserId,
                        NotesId = notes.NotesId,
                        Title = notes.Title,
                        Message = notes.Message,
                        Color = notes.Color,
                        Image = notes.Image,
                        Pin = notes.Pin,
                        Archive = notes.Archive,
                        Trash = notes.Trash,
                        CreatedAt = notes.CreatedAt 
                    };
                    return models;
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
        /// <returns>Response Body of All Notes</returns>
        public GetNotesResponseModel GetAllNotes(long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var user = this.context.NotesTable.FirstOrDefault(e => e.UserId == jwtUserId);
                    GetNotesResponseModel model = new ()
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
        /// <returns>Response Body of Notes with particular notesId</returns>
        public GetNotesResponseModel GetNoteWithId(long notesId, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var user = this.context.NotesTable.FirstOrDefault(i => i.NotesId == notesId && i.UserId == jwtUserId);
                    GetNotesResponseModel model = new ()
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
        /// <returns>Notes with particular notesId</returns>
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
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="updateNotes">The update notes.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Response body of Updated Notes</returns>
        public GetNotesResponseModel UpdateNotes(long notesId, FundooNotes updateNotes, UpdateNotesModel notes, long jwtUserId)
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

                        var user = this.context.NotesTable.FirstOrDefault(i => i.NotesId == notesId && i.UserId == jwtUserId);
                        GetNotesResponseModel model = new ()
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

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public void DeleteNotes(long notesId, FundooNotes notes, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    if (this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId) != null)
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
        /// Pinning the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Response body of pinned notes</returns>
        public GetNotesResponseModel PinningNotes(long notesId, long jwtUserId)
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
                            GetNotesResponseModel model = new ()
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
                            GetNotesResponseModel model = new ()
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
        /// <returns>Response Body of Archoved Notes</returns>
        public GetNotesResponseModel ArchivingNotes(long notesId, long jwtUserId)
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
                            GetNotesResponseModel model = new ()
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
                            GetNotesResponseModel model = new ()
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
        /// Trashing the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Response Body of Trashed notes</returns>
        public GetNotesResponseModel TrashingNotes(long notesId, long jwtUserId)
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
                            GetNotesResponseModel model = new ()
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
                            GetNotesResponseModel model = new ()
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
        /// <param name="notesId">The notes identifier.</param>
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
        ///  Add Image the notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="imageNotes">The image notes.</param>
        /// <param name="image">The image.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Response body of cloudinary image url</returns>
        public ImageResponseModel ImageNotes(long notesId, FundooNotes imageNotes, IFormFile image, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    if (this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId) != null)
                    {
                        Account account = new Account(this.config["Cloudinary:CloudName"], this.config["Cloudinary:APIKey"], this.config["Cloudinary:APISecret"]);
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
                            ImageResponseModel model = new ()
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
        /// Redis catche to the notes.
        /// </summary>
        /// <returns>Response Body of All Notes</returns>
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
