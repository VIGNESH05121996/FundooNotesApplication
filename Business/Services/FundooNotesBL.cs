using Business.Interfaces;
using Common.Models;
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
        public void CreateNotes(NotesModel model)
        {
            try
            {
                this.fundooNotesRL.CreateNotes(model);
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
                return this.fundooNotesRL.GetAllNotes();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public FundooNotes GetNotesWithId(long notesId)
        {
            try
            {
                return this.fundooNotesRL.GetNotesWithId(notesId);
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
                this.fundooNotesRL.UpdateNotes(updateNotes, notes);
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
                this.fundooNotesRL.DeleteNotes(notes);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
