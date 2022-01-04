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
        public void CreateNotes(NotesModel model,long jwtUserId)
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
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<FundooNotes> GetAllNotes(long jwtUserId)
        {
            try
            {
                return this.context.NotesTable.Where(e => e.UserId == jwtUserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FundooNotes GetNotesWithId(long notesId, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.NotesTable.Where(e => e.UserId == jwtUserId);
                if(validUserId != null)
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
                var validUserId = this.context.NotesTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    updateNotes.Title = notes.Title;
                    updateNotes.Message = notes.Message;
                    updateNotes.Color = notes.Color;
                    updateNotes.Image = notes.Image;
                    updateNotes.Archive = notes.Archive;
                    updateNotes.Pin = notes.Pin;
                    updateNotes.Trash = notes.Trash;
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
                var validUserId = this.context.NotesTable.Where(e => e.UserId == jwtUserId);
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
    }
}
