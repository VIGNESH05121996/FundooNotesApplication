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
        public void CreateNotes(NotesModel model,long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.CreateNotes(model,jwtUserId);
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
                return this.fundooNotesRL.GetAllNotes(jwtUserId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public FundooNotes GetNotesWithId(long notesId, long jwtUserId)
        {
            try
            {
                return this.fundooNotesRL.GetNotesWithId(notesId,jwtUserId);
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
                this.fundooNotesRL.UpdateNotes(updateNotes, notes,jwtUserId);
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
                this.fundooNotesRL.DeleteNotes(notes,jwtUserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void PinNotes(long notesId, long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.PinNotes(notesId, jwtUserId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void UnPinNotes(long notesId, long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.UnPinNotes(notesId, jwtUserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ArchiveNotes(long notesId, long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.ArchiveNotes(notesId, jwtUserId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void UnArchiveNotes(long notesId, long jwtUserId)
        {
            try
            {
                this.fundooNotesRL.UnArchiveNotes(notesId, jwtUserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
