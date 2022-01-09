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

        public IEnumerable<FundooNotes> GetAllNotes(long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    return this.context.NotesTable.Where(e => e.UserId == jwtUserId);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

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

        public string PinningNotes(long notesId, long jwtUserId)
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
                        return "Notes Pinned";
                    }
                    var unPinNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Pin == true);
                    if (unPinNotes != null)
                    {
                        unPinNotes.Pin = false;
                        this.context.SaveChanges();
                        return "Notes OnPinned";
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ArchivivingNotes(long notesId, long jwtUserId)
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
                        return "Note Archivied";
                    }
                    var unArchiveNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Archive == true);
                    if (unArchiveNotes != null)
                    {
                        unArchiveNotes.Archive = false;
                        this.context.SaveChanges();
                        return "Note UnArchivied";
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string TrashingNotes(long notesId, long jwtUserId)
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
                        return "Note Moved To Trash";
                    }
                    var unTrashNotes = this.context.NotesTable.FirstOrDefault(e => e.NotesId == notesId && e.Trash == true);
                    if (unTrashNotes != null)
                    {
                        unTrashNotes.Trash = false;
                        this.context.SaveChanges();
                        return "Note UnTrashed";
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

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
