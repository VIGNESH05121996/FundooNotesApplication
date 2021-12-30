using Common.Models;
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
        public void CreateNotes(NotesModel model)
        {
            try
            {
                FundooNotes notes = new()
                {
                    Title = model.Title,
                    Message = model.Message,
                    Color=model.Color,
                    Image=model.Image,
                    Archive=model.Archive,
                    Pin=model.Pin,
                    CreatedAt=model.CreatedAt
                };
                this.context.Add(notes);
                this.context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<FundooNotes> GetAllNotes()
        {
            try
            {
                return this.context.NotesTable.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FundooNotes GetNotesWithId(long notesId)
        {
            try
            {
                return this.context.NotesTable.FirstOrDefault(i => i.NotesId == notesId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateNotes(FundooNotes updateNotes, FundooNotes notes)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteNotes(FundooNotes notes)
        {
            try
            {
                this.context.NotesTable.Remove(notes);
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
