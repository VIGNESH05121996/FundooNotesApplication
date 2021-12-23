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
    public class FundooUserBL : IFundooUserBL<FundooUser>
    {
        public Repository.Interfaces.IFundooUserRL<FundooUser> fundooUserRL;

        public FundooUserBL(IFundooUserRL<FundooUser> fundooUserRL)
        {
            this.fundooUserRL = fundooUserRL;
        }

        public void Register(RegistrationModel model)
        {
            try
            {
                this.fundooUserRL.Register(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<FundooUser> GetAllData()
        {
            try
            {
                return this.fundooUserRL.GetAllData();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public FundooUser GetWithId(long id)
        {
            try
            {
                return this.fundooUserRL.GetWithId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(FundooUser entity)
        {
            try
            {
                this.fundooUserRL.Delete(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(FundooUser dbEntity, FundooUser entity)
        {
            try
            {
                this.fundooUserRL.Update(dbEntity, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
