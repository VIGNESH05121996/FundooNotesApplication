using Business.Interfaces;
using Common.Models;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Services;
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

        public string Login(LoginModel model)
        {
            try
            {
                return this.fundooUserRL.Login(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
